using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



namespace LFU.Views
{
    public class GridViewLoadfile 
    {

        public GridViewLoadfile(string tablename, int counttotalrecords, IEnumerable<string> fieldnamesselected) 
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["PageRowCount"], out this.PageRowCount))
            {
                this.PageRowCount = GridViewLoadfile.PageRowCountDefault; // hardcoded default if config fails
            }

            TableName = tablename;
            TotalRowCount = counttotalrecords;
            LastPage = (TotalRowCount / PageRowCount) + 1;        
            FieldNamesAsDisplayed = fieldnamesselected.ToList<string>();
        } 

        #region "PROPERTIES"

        private List<string> _FieldNamesAsDisplayed;
        public List<string> FieldNamesAsDisplayed 
        {
            get 
            { 
                return _FieldNamesAsDisplayed; 
            }
            
            set 
            {
                _FieldNamesAsDisplayed = value;
            } 
        }

        /// <summary>
        /// Hardcoded default number of Rows will be shown in each page. A configurable value is available in app.config under key=PageRowCount
        /// </summary>
        public static readonly int PageRowCountDefault = 1000;

        /// <summary>
        /// Total number of records in our current table, view, or selection
        /// </summary>
        public int TotalRowCount;

        /// <summary>
        /// Name of the backend table which we are viewing
        /// </summary>
        public string TableName;

        /// <summary>
        /// Will always be ordered by rowid
        /// </summary>
        public string OrderByClause 
        { 
            get 
            { 
                return "rowid"; 
            }
 
            set {} 
        }

        // number of rows in one page
        public int PageRowCount;

        // the page we're viewing
        public int CurrentPage = 1;

        // the last page available
        public int LastPage; 

        #endregion 


        #region "METHODS"

        /// <summary>
        /// Sort gets the database name and table name and grid name and number of rows in each page and the field that we want to sort on
        /// After getting the above information, shows the data into the grid
        /// </summary>
        /// <param name="currentdatagrid"></param>
        /// <param name="startfromrow"></param>
        public void Sort(DataGrid currentdatagrid, int startfromrow)
        {
            string FieldNamesCommaSeparated = null;
            FieldNamesAsDisplayed.Remove("rowid");

            if (FieldNamesAsDisplayed.Count != 0)
            {
                FieldNamesCommaSeparated = "[" + string.Join("],[", FieldNamesAsDisplayed.ToArray()) + "]";
            }

            try
            {
                // Select statement order by the sort field and the rows that we want to show it in the grid
                // *** DO NOT use brackets when building the ORDER BY clause of the following SQL statement.... brackets must be added when the sortState is set on the main form.
                //     We want to sort on >1 field in either asc or desc order, eg, "ORDER BY [Custodian-Name] DESC, [ControlNumber] ASC" ***
                string CommandString = 
                    "SELECT [rowid], "
                    + FieldNamesCommaSeparated
                    + " FROM ["
                    + TableName
                    + "] ORDER BY " 
                    + OrderByClause 
                    + " LIMIT "
                    + PageRowCount 
                    + " OFFSET " 
                    + startfromrow;

                Log.ErrorLog.AddMessage("Sorting table " + TableName + " using command: " + CommandString);

                //build the command
                using (SQLiteCommand MySelectCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    SQLiteDataAdapter MySqliteDataAdapter = new SQLiteDataAdapter(MySelectCommand);

                    //Building the data table
                    DataTable Dt = new DataTable(TableName);

                    //Filling the data table
                    MySqliteDataAdapter.Fill(Dt);

                    currentdatagrid.ItemsSource = Dt.DefaultView;
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed on Sort operation" + Environment.NewLine + Ex.Message);
                global::System.Windows.MessageBox.Show(Ex.Message);
            }
            
        }


        /// <summary>
        /// Update the backing database
        /// </summary>
        /// <param name="setvalue"></param>
        /// <param name="rownumber"></param>
        /// <param name="columnname"></param>
        public void Update(string setvalue, int rownumber, string columnname)
        {
            try
            {
                string CommandString = 
                    "UPDATE ["
                    + TableName
                    + "] SET [" 
                    + columnname 
                    + "] = '"
                    + setvalue 
                    + "' WHERE [_rowid_] = " 
                    + rownumber;

                Log.ErrorLog.AddMessage("Updating table " + TableName + " using command: " + CommandString);

                //build the command
                using (SQLiteCommand MyUpdateCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    MyUpdateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed on Update operation" + Environment.NewLine + Ex.Message);
                global::System.Windows.MessageBox.Show(Ex.Message);
            }         
        }


        /// <summary>
        /// View search gets the database and the grid name and show the search result into the grid and gives number of hits
        /// </summary>
        /// </summary>
        /// <param name="GridName"></param>
        /// <param name="searchcolumn"></param>
        /// <param name="searchterm"></param>
        /// <param name="fieldnames">Lsit of fields from the View, in same order as View</param>
        /// <returns>Number of hits</returns>
        public int Search(DataGrid currentdatagrid, string searchcolumn, string searchterm)
        {
            Log.ErrorLog.AddMessage("Searching column " + searchcolumn + " on table " + TableName);

            int NumberOfHits = 0;
            Object ResutlHits = new Object();

            try
            {
                string selectSQL = 
                    "SELECT [rowid], * FROM ["
                    + TableName 
                    + "] WHERE [" 
                    + searchcolumn 
                    + "] LIKE '%"
                    + searchterm 
                    + "%'";

                // building the command  
                using (SQLiteCommand MySelectCommand = new SQLiteCommand(selectSQL, Db.Connect.Connection))
                {
                    using (SQLiteDataAdapter SQLiteAdapOB = new SQLiteDataAdapter(MySelectCommand))
                    {
                        DataTable DT = new DataTable(TableName);
                        Log.ErrorLog.AddMessage("Executing search with command: " + selectSQL);
                        SQLiteAdapOB.Fill(DT);
                        currentdatagrid.ItemsSource = DT.DefaultView;
                        NumberOfHits = DT.Rows.Count;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed on Search operation" + Environment.NewLine + Ex.Message);
                global::System.Windows.MessageBox.Show(Ex.Message);
            }

            return NumberOfHits;
        }



        #endregion

    }
}
