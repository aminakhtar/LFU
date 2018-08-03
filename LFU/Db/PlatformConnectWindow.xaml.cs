using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for PlatformConnectWindow.xaml
    /// </summary>
    public partial class PlatformConnectWindow : Window
    {

        public PlatformConnectWindow()
        {
            InitializeComponent();
        }

        private string ConnectionStringTemplate;
        private string[] Servers { get; set; }
        private string[] Databases { get; set; }
        public string ConnectionName { get; set; }
        public string ErrorMessage { get; set; }

        #region "INTERFACE EVENTS"

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // data source={0}; initial catalog={1}; integrated security=true; application name=LFU
            ConnectionStringTemplate = ConfigurationManager.ConnectionStrings["LawDatabase"].ConnectionString; 

            Servers = ConfigurationManager.AppSettings["ProcessingPlatformServers"].Split(';');
            this.cmboPlatformServers.ItemsSource = Servers;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string server = this.cmboPlatformServers.SelectedItem.ToString();
            string db = this.cmboPlatformDatabases.SelectedItem.ToString();

            if (server != null && db != null)
            {
                string connectionstring = string.Format(
                    ConnectionStringTemplate,
                    server,
                    db
                    );

                try
                {
                    SqlConnection MyPlatformConnection = new SqlConnection(connectionstring);
                    MyPlatformConnection.Open();
                    if (Db.Connect.PlatformConnections.ContainsKey(db))
                    {
                        ErrorMessage = db + " already has a connection";
                        this.DialogResult = false;
                        this.Close();
                        return;
                    }
                    Db.Connect.PlatformConnections.Add(db, MyPlatformConnection);
                    ConnectionName = server + "." + db;
                    this.DialogResult = true;
                    this.Close();
                }
                catch (SqlException SEx)
                {
                    this.DialogResult = false;
                    ErrorMessage =
                        "Platform connect failed to connect to server: " + server +
                        " database: " + db + Environment.NewLine +
                        "connection string: " + connectionstring + Environment.NewLine + 
                        SEx.Message + Environment.NewLine +
                        SEx.StackTrace;

                    Log.ErrorLog.AddMessage(ErrorMessage);
                    this.Close();
                }
            }
        }

        private void cmboPlatformServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string server = this.cmboPlatformServers.SelectedItem.ToString();

            // connect to master to get names of databases on this server
            string connectionstring = string.Format(
                ConnectionStringTemplate,
                server,
                "master"
                );

            using (SqlConnection MyConnection = new SqlConnection(connectionstring))
            {
                MyConnection.Open();

                // restrict list of database names to non-system databases
                string commandstring = "SELECT [name] FROM [sys].[databases] WHERE [owner_sid] > 0x01 ORDER BY [name]; ";

                using (SqlCommand MyCommand = new SqlCommand(commandstring, MyConnection))
                {
                    using (SqlDataReader MyReader = MyCommand.ExecuteReader())
                    {
                        List<string> dbnames = new List<string>();
                        while (MyReader.Read())
                        {
                            dbnames.Add(MyReader.GetString(0));
                        }
                        Databases = dbnames.ToArray<string>();
                        this.cmboPlatformDatabases.ItemsSource = Databases;
                    }
                }
            }

        }

        #endregion




    }
}
