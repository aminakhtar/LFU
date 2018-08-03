using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pdc.Loadfiles;
using System.Reflection;

namespace LFU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region "CONSTRUCTORS"

        
        public MainWindow()
        {
            // let's see how long it takes to load the application and get ready for user
            Log.Timer.Start();

            InitializeComponent();

            assembly = Assembly.GetEntryAssembly();
            this.Title = this.Title + "  Version: " + assembly.GetName().Version.ToString();
        }

        private Assembly assembly;

        #endregion



        #region "FIELDS"

        private SqlConsoleWindow SqlConsole;
        private MaintWindow Maint;

        #endregion


        private void OnStatusUpdate(object sender, Log.StatusEventArgs e)
        {
            this.tblStatus.Text = e.Message;
        }


        private void BuildNewLoadfileView(LoadfileBase selectedloadfile)
        {
            try
            {
                Frame NewLoadfileFrame = new Frame();
                Views.LoadfilePage NewLoadfilePage = new Views.LoadfilePage(selectedloadfile);
                // NewLoadfilePage.StatusUpdate += OnStatusUpdate; 
                NewLoadfilePage.StatusUpdate += OnRefreshMainWindow;
                NewLoadfilePage.LoadLoadfile();
                NewLoadfileFrame.Content = NewLoadfilePage;                

                // we have to add a textblock to the tabitem header because the table names contain underscores
                // default tabitem header will treat FIRST underscore as indication of following character being a shortcut like ampersand used to function in winforms
                
                TextBlock NewTabTitle = new TextBlock();
                NewTabTitle.Text = selectedloadfile.FileInformation.Name;

                Button NewTabCloseButton = new Button();
                NewTabCloseButton.Content = (char)215;
                NewTabCloseButton.FontSize = 10;
                NewTabCloseButton.Height = 15;
                NewTabCloseButton.Width = 15;
                NewTabCloseButton.Click += tabHeaderCloseButton_OnClick;
                NewTabCloseButton.Margin = new Thickness(5, 0, 0, 0);
                NewTabCloseButton.Padding = new Thickness(0);
                NewTabCloseButton.BorderThickness = new Thickness(0);
                NewTabCloseButton.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                NewTabCloseButton.ToolTip = "Close";

                StackPanel NewTabHeader = new StackPanel();
                NewTabHeader.Orientation = Orientation.Horizontal;
                NewTabHeader.Children.Add(NewTabTitle);
                NewTabHeader.Children.Add(NewTabCloseButton);        

                TabItem NewTab = new TabItem();
                NewTab.Header = NewTabHeader;
                NewTab.Content = NewLoadfileFrame;
                this.tabcontrolMain.Items.Add(NewTab);
                this.tabcontrolMain.SelectedItem = NewTab;

            }
            catch (Exception Ex)
            {
                this.tblStatus.Text = "Failed to open new loadfile.";
                Log.ErrorLog.AddMessage("Failed to open new loadfile.");
            }

            if (this.tabcontrolMain.Items.Count > 0)
            {
                this.btnSqlConsole.IsEnabled = true;
            }
            else
            {
                this.btnSqlConsole.IsEnabled = false;
            }
        }



        #region "FILE INTERFACE EVENTS"

        /// <summary>
        /// Open the OpenLoadfileDialog to browse the filesystem for a loadfile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenLoadfileDialog Olfd = new OpenLoadfileDialog();

            if (Olfd.ShowDialog() == true)
            {
                BuildNewLoadfileView(Olfd.SelectedLoadfile);
            }
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (File.Exists(files[0]))
                {
                    OpenLoadfileDialog Olfd = new OpenLoadfileDialog(files[0]);

                    if (Olfd.ShowDialog() == true)
                    {
                        BuildNewLoadfileView(Olfd.SelectedLoadfile);
                    }
                }
                else
                {
                    this.tblStatus.Text = "File does not exist: \"" + files[0] + "\"";
                }
            }
        }

        private void File_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void File_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region "INTERFACE EVENTS"

        /// <summary>
        /// Create temporary database file, build the delimiter characters list, load some defaults from config, enable some controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DelimiterCharacters.Create();
            this.tblStatus.Text = "Application loaded in " + Log.Timer.ElapsedTime().ToString();
        }

        /// <summary>
        /// Clean up. Delete the temporary database file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SqlConsole != null)
            {
                SqlConsole.Close();
            }

            try
            {
                if (Db.Connect.DatabasePath == ":memory:")
                {
                    Db.Connect.Connection.Close();
                }
                else if (File.Exists(Db.Connect.DatabasePath))
                {
                    Db.Connect.Connection.Close();
                    File.Delete(Db.Connect.DatabasePath);
                    Log.ErrorLog.AddMessage("Deleted database file: " + Db.Connect.DatabasePath);
                }
            }
            catch (Exception Ex)
            {
                // do nothing, we can delete orphaned databases from the Maint window
                Log.ErrorLog.AddMessage(
                    "Error cleaning-up database environment "
                    + Db.Connect.DatabasePath
                    + Environment.NewLine
                    + Ex.Message
                    + Environment.NewLine + Ex.StackTrace
                    );
            }

            Log.ErrorLog.Close(); // 2015-10-02 ds: not sure why this is a problem all of a sudden
        }


        public void tabHeaderCloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            UIElement item = (UIElement)sender; // sender is the button
            var closeme = LogicalTreeHelper.GetParent(item);
            this.tabcontrolMain.Items.Remove(LogicalTreeHelper.GetParent(closeme));

        }

        private void btnMaint_Click(object sender, RoutedEventArgs e)
        {
            Maint = new MaintWindow();
            Maint.ShowDialog();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow About = new AboutWindow();
            About.ShowDialog();
        }

        private void btnPlatformConnect_Click(object sender, RoutedEventArgs e)
        {
            Db.PlatformConnectWindow platformconnectwindow = new Db.PlatformConnectWindow();
            if (platformconnectwindow.ShowDialog() == true)
            {
                this.tblStatus.Text = "Connected to " + platformconnectwindow.ConnectionName;
            }
            else if (platformconnectwindow.DialogResult == false)
            {
                this.tblStatus.Text = platformconnectwindow.ErrorMessage;
            }
        }

        #endregion

        #region "SQL CONSOLE"

        private void btnSqlConsole_Click(object sender, RoutedEventArgs e)
        {
            SqlConsole = new SqlConsoleWindow(this.tabcontrolMain);
            SqlConsole.Show(); // let them open as many as they want
            SqlConsole.SqlConsoleRefresher += OnRefreshMainWindow;

        }


        public void OnRefreshMainWindow(object sender, Log.StatusEventArgs e)
        {
            this.tblStatus.Text = e.Message;
        }



        #endregion




    }
}
