using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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

namespace LFU
{
    /// <summary>
    /// Interaction logic for MaintWindow.xaml
    /// </summary>
    public partial class MaintWindow : Window
    {
        public MaintWindow()
        {
            InitializeComponent();

            PlatformConnections = new List<SqlConnection>();

            foreach (SqlConnection C in Db.Connect.PlatformConnections.Values)
            {
                PlatformConnections.Add(C);
            }

            this.dgPlatformDatabaseConnections.ItemsSource = PlatformConnections;
            
        }

        List<FileInfo> LfuFiles;
        List<SqlConnection> PlatformConnections;

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (FileInfo fi in this.dgDatabaseFiles.SelectedItems)
            {
                try
                {
                    File.Delete(fi.FullName);
                    this.tblStatus.Text = "Deleted " + fi.FullName;
                }
                catch (Exception Ex)
                {
                    this.tblStatus.Text = Ex.Message;
                    Log.ErrorLog.AddMessage("Error deleting database from Maint Window: " + fi.FullName + Environment.NewLine + Ex.Message);
                }
            }

            LfuFiles = Scan(
                 new List<string>() {
                        System.IO.Path.GetTempPath(),
                        ConfigurationManager.AppSettings["AlternativeDatabaseStoragePath"].ToString()
                        }
                );

            this.dgDatabaseFiles.ItemsSource = LfuFiles;
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                LfuFiles = Scan(
                    new List<string>() {
                        System.IO.Path.GetTempPath(),
                        ConfigurationManager.AppSettings["AlternativeDatabaseStoragePath"].ToString()
                        }
                    );

                this.dgDatabaseFiles.ItemsSource = LfuFiles;
                this.dgDatabaseFiles.DisplayMemberPath = "FullName";
                this.dgDatabaseFiles.SelectedValuePath = "FullName";
            }
            catch (Exception Ex)
            {
                this.tblStatus.Text = Ex.Message;
            }
        }

        private List<FileInfo> Scan(List<string> paths)
        {
            List<FileInfo> Results = new List<FileInfo>();

            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                {
                    Results.AddRange(
                        new DirectoryInfo(path).GetFiles("LFU*.*", SearchOption.TopDirectoryOnly)
                        );
                }
            }

            return Results;
        }

    }
}
