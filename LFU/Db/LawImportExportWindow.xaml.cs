using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LFU.Db
{
    /// <summary>
    /// Interaction logic for ImportFromLaw.xaml
    /// </summary>
    public partial class LawImportExport : Window
    {

        #region "CONSTRUCTOR"

        public LawImportExport(string tablename, IEnumerable<string> fieldnamesasdisplayed)
        {           
            InitializeComponent();            

            TableName = tablename;
            FieldNamesAsDisplayed = fieldnamesasdisplayed.ToList<string>();
            this.DataContext = this; 

            BatchSize = int.Parse(ConfigurationManager.AppSettings["SqlBatchSize"]);

            if (BatchSize < 1)
            {
                BatchSize = 5000; // default to 5000
            }

        }

        #endregion


        #region "FIELDS AND PROPERTIES"

        public int UpdateCount { get; set; }

        /// <summary>
        /// List of field names from the loadfile on which we're currently working. This property must be public for binding to work.
        /// </summary>
        public List<string> FieldNamesAsDisplayed { get; set; }

        /// <summary>
        /// Name of the table holding the current loadfile's data
        /// </summary>
        private string TableName { get; set; }

        // import
        private SQLiteCommand UpdateCommandBackend { get; set; }
        private SQLiteTransaction MyTransactionBackend { get; set; }

        // export
        private SqlCommand UpdateCommandPlatform { get; set; }
        private SqlTransaction MyTransactionPlatform { get; set; }

        private BackgroundWorker Bgw;
        private int BatchSize = 0;

        private string _UserAddedWhereClause;
        private string UserAddedWhereClause
        {
            get
            {
                return _UserAddedWhereClause ?? ""; // don't allow null
            }

            set
            {
                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        _UserAddedWhereClause = " AND (" + value + ")";
                    }
                }
            }
        }

        #endregion


        #region "LOCAL HELPER CLASSES"

        private class QueryResult
        {
            public QueryResult() { }

            public QueryResult(object[] values) 
            {
                IdentityValue = values[0].ToString();

                Values = new List<string[]>();

                for (int i = 1; i < values.Length; i++ )
                {
                    Values.Add(
                        new string[2] {
                            FieldNames[i-1], 
                            values[i].ToString().Replace("'", "''")
                        }
                    );
                }
            }

            public static int FieldCount
            {
                get
                {
                    return FieldNames.Length;
                }
            }

            public static string[] FieldNames;

            public string IdentityValue;
            public List<string[]> Values;

        }

        /// <summary>
        /// For sending job parameters to the background worker
        /// </summary>
        private class JobParams
        {
            public JobParams()
            {
            }

            /// <summary>
            /// If IsImport == false then our process is to Export
            /// </summary>
            public bool IsImport { get; set; }
            public string ConnectionName { get; set; }
            public string IdentityLoadfile { get; set; }
            public string IdentityCase { get; set; }
            public string[] UpdateFieldPairs { get; set; } // semicolon separated, loadfile;case

            private string _WhereClause;
            public string WhereClause 
            {
                get
                {
                    return " AND " + _WhereClause;
                }

                set
                {
                    _WhereClause = value;
                }
            }

            /// <summary>
            /// Used for Export to LAW. We need to know name of the backend sqlite 
            /// </summary>
            public string TableName { get; set; }

        }

        /// <summary>
        /// Returned when background worker is complete
        /// </summary>
        private class JobCompleted
        {
            public JobCompleted() { }

            public JobCompleted(double countupdatedrecords)
            {
                CountUpdatedRecords = countupdatedrecords;
            }

            public bool IsImport { get; set; }
            public double CountUpdatedRecords { get; set; }
        }

        #endregion


        #region "METHODS"

        private int UpdatePlatform(string identitycase, QueryResult queryresult, SqlConnection platformconnection)
        {
            int CountUpdatedRecords = 0;

            var setclauses = from V in queryresult.Values
                             select "[" + V[0] + "] = '" + V[1] + "'";

            string commandstring =
                "UPDATE " +
                "[tblDoc] SET " +
                string.Join(", ", setclauses) +
                " WHERE [" +
                identitycase +
                "] = '" +
                queryresult.IdentityValue +
                "';";

            UpdateCommandPlatform = new SqlCommand(commandstring, platformconnection, MyTransactionPlatform);
            CountUpdatedRecords = UpdateCommandPlatform.ExecuteNonQuery();

            return CountUpdatedRecords;

        }

        private int UpdateBackend(string identityloadfile, QueryResult queryresult)
        {
            int CountUpdatedRecords = 0;

            var setclauses = from V in queryresult.Values
                             select "[" + V[0] + "] = '" + V[1] + "'";

            string commandstring =
                "UPDATE " + 
                "[" +
                TableName +
                "] SET " + 
                string.Join(", ", setclauses) + 
                " WHERE [" +
                identityloadfile +
                "] = '" +
                queryresult.IdentityValue +
                "';";

            UpdateCommandBackend = new SQLiteCommand(commandstring, Db.Connect.Connection, MyTransactionBackend);
            CountUpdatedRecords = UpdateCommandBackend.ExecuteNonQuery();

            return CountUpdatedRecords;
        }

        private IEnumerable<string> Next(string loadfileidentity)
        {
            string commandstring =
                "SELECT [" +
                loadfileidentity +
                "] FROM [" +
                TableName +
                "]; ";

            List<string> Ids = new List<string>();

            using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Db.Connect.Connection))
            {
                using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                {
                    while (MyReader.Read())
                    {
                        if (MyReader.IsDBNull(0))
                        {
                            continue;
                        }

                        // Ids.Add(MyReader.GetString(0));
                        yield return MyReader.GetString(0);
                    }
                }
            }

            /*
            foreach (string id in Ids)
            {
                yield return id;
            }*/

        }


        #endregion


        #region "INTERFACE EVENTS"

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = UpdateCount > 0 ? true : false;
            this.Close();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

            if (this.rdoImport.IsChecked != true && this.rdoExport.IsChecked != true)
            {
                this.tblStatus.Text = "Are you trying to import or export?";
            }
            else
            {
                if (this.cmboConnections.SelectedItem != null
                    && this.cmboIdentityLoadfile.SelectedItem != null
                    && this.cmboIdentityCase.SelectedItem != null
                    && this.stackUpdateFields.Children.Count > 0)
                {
                    // semicolon-separated list of pairs of fields, ie, loadfileField;caseField
                    var UpdateFieldPairs = from UpdateFieldPairControl Ufpc in this.stackUpdateFields.Children
                                           select Ufpc.LoadfileField + ";" + Ufpc.CaseField;

                    this.progBar.Visibility = System.Windows.Visibility.Visible;

                    JobParams MyJobParams = new JobParams();
                    MyJobParams.ConnectionName = this.cmboConnections.SelectedItem.ToString();
                    MyJobParams.IdentityCase = this.cmboIdentityCase.SelectedItem.ToString();
                    MyJobParams.IdentityLoadfile = this.cmboIdentityLoadfile.SelectedItem.ToString();
                    MyJobParams.UpdateFieldPairs = UpdateFieldPairs.ToArray();
                    MyJobParams.WhereClause = this.tbWhereClauseAdditionalExpressions.Text;

                    // process on background thread
                    this.progBar.Value = 0;
                    Bgw = new BackgroundWorker();
                    
                    Bgw.ProgressChanged += Bgw_ProgressChanged;
                    Bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
                    Bgw.WorkerReportsProgress = true;
                    Bgw.WorkerSupportsCancellation = true;

                    // import, download from database
                    if (this.rdoImport.IsChecked == true)
                    {
                        this.tblStatus.Text = "Importing from database....";
                        MyJobParams.IsImport = true;
                        Bgw.DoWork += Bgw_DoWorkImport;
                    }
                    // export, upload to database, aka overlay
                    else
                    {
                        this.tblStatus.Text = "Exporting to database....";
                        MyJobParams.IsImport = false;
                        MyJobParams.TableName = this.TableName;
                        Bgw.DoWork += Bgw_DoWorkExport;
                    }

                    this.dockInterface.IsEnabled = false;
                    this.dockProgBar.IsEnabled = false;

                    Bgw.RunWorkerAsync(MyJobParams);

                }
                else
                {
                    this.tblStatus.Text = "Missing parameters!";
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get connections
            foreach (SqlConnection C in Db.Connect.PlatformConnections.Values)
            {
                this.cmboConnections.Items.Add(C.Database);
            }

            if (Db.Connect.PlatformConnections.Values.Count > 0)
            {
                this.cmboConnections.SelectedItem = this.cmboConnections.Items[0];
            }

            // get loadfile fields
            //this.cmboIdentityLoadfile.ItemsSource = FieldNamesAsDisplayed;
            //this.cmboUpdateLoadfile.ItemsSource = FieldNamesAsDisplayed;

        }

        private void cmboConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string db = this.cmboConnections.SelectedItem.ToString();

            // clear the case items from their comboboxes when we select a new connection
            this.cmboIdentityCase.Items.Clear();
            this.cmboUpdateCase.Items.Clear();

            // populate the other comboboxes when we select a new connection
            string collectfieldscommandstring = 
                "SELECT [COLUMN_NAME] FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME] = 'tblDoc' AND [TABLE_CATALOG] = '"
                + db
                + "' ORDER BY [COLUMN_NAME]; ";

            using (SqlCommand MyCommand = new SqlCommand(collectfieldscommandstring, Db.Connect.PlatformConnections[db]))
            {
                using (SqlDataReader MyReader = MyCommand.ExecuteReader())
                {
                    while (MyReader.Read())
                    {
                        this.cmboIdentityCase.Items.Add(MyReader.GetString(0));
                        this.cmboUpdateCase.Items.Add(MyReader.GetString(0));
                    }
                }
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmboUpdateLoadfile.SelectedItem != null && this.cmboUpdateCase.SelectedItem != null)
            {
                UpdateFieldPairControl Ufpc = new UpdateFieldPairControl(this.cmboUpdateLoadfile.SelectedItem.ToString(), this.cmboUpdateCase.SelectedItem.ToString());

                // don't add same item twice
                foreach (UpdateFieldPairControl added in this.stackUpdateFields.Children)
                {
                    if (added.LoadfileField == Ufpc.LoadfileField && added.CaseField == Ufpc.CaseField)
                    {
                        return;
                    }
                }

                // add this one if its not already in the collection
                this.stackUpdateFields.Children.Add(Ufpc);
            }
        }

        #endregion


        #region "BACKGROUNDWORKER EVENTS"

        private void Bgw_DoWorkExport(object sender, DoWorkEventArgs e)
        {
            double CountTotalRecords = (double)Db.Connect.RecordCount(TableName);
            double CountUpdatedRecords = 0.0;
            double CountProgress = 0.0;

            JobParams Jp = (JobParams)e.Argument;

            var F = from S in Jp.UpdateFieldPairs
                    select S.Split(';')[0];

            QueryResult.FieldNames = F.ToArray<string>();

            Bgw.ReportProgress(0, "Querying backend database.... ");

            string UpdateCaseFields = 
                "[" +
                string.Join("], [", from S in Jp.UpdateFieldPairs select S.Split(';')[1]) +
                "] ";

            string commandstring =
                "SELECT [" + 
                Jp.IdentityLoadfile + 
                "], " + 
                UpdateCaseFields + 
                "FROM [" +
                Jp.TableName + 
                "] WHERE [" +
                Jp.IdentityLoadfile + 
                "] IS NOT NULL " +
                Jp.WhereClause +   
                ";";

            SqlConnection PlatformConnection = Db.Connect.PlatformConnections[Jp.ConnectionName];
            MyTransactionPlatform = PlatformConnection.BeginTransaction();

            // collect only values we need from the backend database
            using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Db.Connect.Connection))
            {
                using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                {
                    while (MyReader.Read())
                    {
                        
                        object[] V = new object[MyReader.FieldCount];
                        MyReader.GetValues(V);

                        for (int i = 1; i < V.Length; i++)
                        {
                            if (MyReader.IsDBNull(i))
                            {
                                V[i] = "";
                            }
                        }
                        
                        CountUpdatedRecords += (double)UpdatePlatform(Jp.IdentityCase, new QueryResult(V), PlatformConnection);

                        if (++CountProgress / CountTotalRecords > 0.01) // show progress every 1 percent
                        {
                            Bgw.ReportProgress(
                                (int)((CountUpdatedRecords / CountTotalRecords) * 100.0),
                                "Updating " + ((double)CountUpdatedRecords).ToString("#,##0") + " records.... "
                                );
                            CountProgress = 0.0;
                        }

                        if (CountUpdatedRecords % BatchSize == 0)
                        {
                            MyTransactionPlatform.Commit();
                            MyTransactionPlatform = PlatformConnection.BeginTransaction();
                        }

                    }
                }
            }

            e.Result = new JobCompleted(CountUpdatedRecords);
            MyTransactionPlatform.Commit();
            UpdateCommandPlatform.Dispose();

        }

        private void Bgw_DoWorkImport(object sender, DoWorkEventArgs e)
        {
            double CountTotalRecords = (double)Db.Connect.RecordCount(TableName);
            double CountUpdatedRecords = 0.0;
            double CountProgress = 0.0;

            JobParams Jp = (JobParams)e.Argument;

            var F = from S in Jp.UpdateFieldPairs
                    select S.Split(';')[0];

            QueryResult.FieldNames = F.ToArray<string>();

            Bgw.ReportProgress(0, "Querying processing platform database.... ");

            string UpdateCaseFields = 
                "[" +
                string.Join("], [", from S in Jp.UpdateFieldPairs select S.Split(';')[1]) + 
                "] ";

            string commandstring =
                "SELECT [" +
                Jp.IdentityCase +
                "], " +
                UpdateCaseFields +
                " FROM [tblDoc] WHERE [" +  // why don't we make table a parameter?
                Jp.IdentityCase +
                "] IS NOT NULL " +          // never check records where the identity field's value is null
                Jp.WhereClause +            // need to add "AND (" to the front of user-added where expressions and ")" to the end
                ";";

            // create an index in sqlite database so updates are faster
            try
            {
                Db.Connect.ApplyIndex(TableName, Jp.IdentityLoadfile);
            }
            catch (Exception Ex)
            {
                this.tblStatus.Text = Ex.Message;
            }

            // update the local backing database with the values collected from the processing platform
            MyTransactionBackend = Db.Connect.Connection.BeginTransaction();

            try
            {
                // collect only values we need from the processing platform database
                using (SqlCommand MyCommand = new SqlCommand(commandstring, Db.Connect.PlatformConnections[Jp.ConnectionName]))
                {
                    using (SqlDataReader MyReader = MyCommand.ExecuteReader())
                    {
                        while (MyReader.Read())
                        {
                            object[] V = new object[MyReader.FieldCount];
                            MyReader.GetSqlValues(V);

                            for (int i = 1; i < V.Length; i++)
                            {
                                if (MyReader.IsDBNull(i))
                                {
                                    V[i] = "";
                                }
                            }

                            CountUpdatedRecords += (double)UpdateBackend(Jp.IdentityLoadfile, new QueryResult(V));

                            if (++CountProgress / CountTotalRecords > 0.01) // show progress every 1 percent
                            {
                                Bgw.ReportProgress(
                                    (int)((CountUpdatedRecords / CountTotalRecords) * 100.0),
                                    "Updating " + ((double)CountUpdatedRecords).ToString("#,##0") + " records.... "
                                    );
                                CountProgress = 0.0;
                            }

                            if (CountUpdatedRecords % BatchSize == 0)
                            {
                                MyTransactionBackend.Commit();
                                MyTransactionBackend = Db.Connect.Connection.BeginTransaction();
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Bgw.ReportProgress(
                    (int)((CountUpdatedRecords / CountTotalRecords) * 100.0),
                    "ERROR! Import did not finish: " + Ex.Message
                    );
            }

            e.Result = new JobCompleted(CountUpdatedRecords);
            MyTransactionBackend.Commit();
            UpdateCommandBackend.Dispose();
        }

        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progBar.Value = e.ProgressPercentage;
            this.tblStatus.Text = (string)e.UserState;
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progBar.Visibility = System.Windows.Visibility.Collapsed;

            JobCompleted Jc = (JobCompleted)e.Result;

            this.tblStatus.Text = "Updated " + Jc.CountUpdatedRecords.ToString("#,##0") + " records";

            UpdateCount += (int)Jc.CountUpdatedRecords;

            if (Jc.IsImport)
            {
                Bgw.DoWork -= Bgw_DoWorkImport;
            }
            else
            {
                Bgw.DoWork -= Bgw_DoWorkExport;
            }

            Bgw.ProgressChanged -= Bgw_ProgressChanged;
            Bgw.RunWorkerCompleted -= Bgw_RunWorkerCompleted;

            // reset interface
            this.stackUpdateFields.Children.Clear();
            this.dockInterface.IsEnabled = true;
            this.dockProgBar.IsEnabled = true;

        }

        #endregion


    }
}
