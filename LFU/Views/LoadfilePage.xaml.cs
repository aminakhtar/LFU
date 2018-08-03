using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Pdc.Loadfiles;
using System.Text.RegularExpressions;

namespace LFU.Views
{

    /// <summary>
    /// Interaction logic for LoadfilePage.xaml
    /// </summary>
    public partial class LoadfilePage : Page
    {

        #region "CONSTRUCTORS"

        public LoadfilePage(LoadfileBase selectedloadfile)
        {
            InitializeComponent();
            this.Loadfile = selectedloadfile;
            this.tbFilepath.IsReadOnly = true;
        }

        public LoadfilePage()
        {
            InitializeComponent();

            // color coded for ad hoc select
            this.tabcontrolGridViews.Background = Brushes.AntiqueWhite;
        }

        #endregion


        #region "FIELDS"

        public LoadfileBase Loadfile;
        public event Log.StatusEventHandler StatusUpdate;
        private string SelectCommandString;

        #endregion



        #region "PROPERTIES" 

        public IViewPage CurrentView
        {
            get
            {
                // return (TablePage)this.tabcontrolGridViews.SelectedContent; 
                return (IViewPage)this.tabcontrolGridViews.SelectedContent; 
            }
        }

        #endregion



        #region "METHODS"

        public void LoadLoadfile()
        {
            Status("Loading " + Loadfile.FileInformation.Name + ".... ");
            
            try
            {
                Log.Timer.Start();
                // create database
                if (string.IsNullOrEmpty(Db.Connect.DatabasePath))
                {
                    Db.Connect.Initialize(this.Loadfile.FileInformation.Length);
                }

                // read loadfile into database
                using (Db.Load Loader = new Db.Load())
                {
                    Loader.LoadToDatabase(Loadfile);
                    this.tbFilepath.Text = this.Loadfile.FileInformation.FullName;

                    // add TablePage tabs for data and errors
                    GridViewLoadfile DataGridView = new GridViewLoadfile(Loader.TableName, Loader.CountTotalRecords, Loadfile.FieldNamesSelected);
                    Views.TablePage DataPage = new Views.TablePage(DataGridView, Loadfile);
                    DataPage.StatusUpdate += OnStatusUpdate;
                    DataPage.TabName = Loader.TableName.Replace("_", "__"); // the underscores can indicate a hotkey so escape them with a second underscore
                    Frame DataPageFrame = new Frame();
                    DataPageFrame.Content = DataPage;
                    TabItem DataPageTabItem = new TabItem(); 
                    DataPageTabItem.Content = DataPage;
                    DataPageTabItem.Header = DataPage.TabName;
                    this.tabcontrolGridViews.Items.Add(DataPageTabItem);

                    // we need to add a tab for errors as well
                    GridViewLoadfile ErrorGridView = new GridViewLoadfile(Loader.ErrorTableName, Loader.CountTotalRecordsErrors, new List<string>() { "Position", "Error", "Line" });
                    Views.TablePage ErrorPage = new Views.TablePage(ErrorGridView, Loadfile);
                    ErrorPage.TabName = Loader.ErrorTableName.Replace("_", "__"); // the underscores can indicate a hotkey so escape them with a second underscore
                    Frame ErrorPageFrame = new Frame();
                    ErrorPageFrame.Content = ErrorPage;
                    TabItem ErrorPageTabItem = new TabItem();
                    ErrorPageTabItem.Content = ErrorPageFrame;
                    ErrorPageTabItem.Header = ErrorPage.TabName;
                    this.tabcontrolGridViews.Items.Add(ErrorPageTabItem);

                    this.btnSave.IsEnabled = true;

                    Status("Load is complete with "
                        + (Loader.CountBadRecords + Loader.CountErrors).ToString("#,##0")
                        + " errors: "
                        + Loadfile.FileInformation.FullName
                        + " ("
                        + Log.Timer.ElapsedTime().ToString()
                        + ")");
                }

            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error on LoadLoadfile operation" + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
                Status("Error on LoadLoadfile operation (" + Log.Timer.ElapsedTime().ToString() + ")");
            }

        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectcommandstring"></param>
        /// <param name="rowcount"></param>
        public void LoadAdHocSelect(string selectcommandstring, out int rowcount)
        {

            SelectCommandString = selectcommandstring;

            // build a new grid view
            GridViewSelect Dgv = new GridViewSelect(selectcommandstring, out rowcount);

            // build a new page to use the grid view
            Views.AdHocSelectPage AdHocSelectPage = new AdHocSelectPage(Dgv);

            Frame DataPageFrame = new Frame();
            DataPageFrame.Content = AdHocSelectPage;
            TabItem DataPageTabItem = new TabItem();
            DataPageTabItem.Content = AdHocSelectPage;
            DataPageTabItem.Header = "Ad Hoc Select";
            this.tabcontrolGridViews.Items.Add(DataPageTabItem);

            // need to be able to generate a loadfile using pdc.loadfiles

            /*
            this.Loadfile = new ConcordanceDat(NewLoadfileFilepath);
            
            this.tbFilepath.IsReadOnly = false;
            this.tbFilepath.Text = NewLoadfileFilepath; 
            */
        }


        /// <summary>
        /// Broadcast status messages back to the MainWindow
        /// </summary>
        /// <param name="message">Simple message from the current view</param>
        public void Status(string message)
        {
            if (StatusUpdate != null)
            {
                StatusUpdate(this, new Log.StatusEventArgs(message));
            }
        }

        /// <summary>
        /// Service events from children by repeating event message in this window's status event (which will display on main window)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnStatusUpdate(object sender, Log.StatusEventArgs e)
        {
            if (StatusUpdate != null)
            {
                StatusUpdate(this, new Log.StatusEventArgs(e.Message));
            }
        }

        #endregion



        #region "INTERFACE EVENTS"

        // Save, SaveAs, or Export
        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            // we should be checking the Loadfile object
            if (Loadfile == null)
            {
                // Get the table name from the command in SQL Console
                #region Select Ad-Hoc

                string pattern = @".*\s*FROM\s*(?<tblname>\w*)\s*";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = rgx.Match(SelectCommandString);
                CurrentView.TableName = match.Groups["tblname"].Value.ToString();

                #endregion

                string NewLoadfileFilepath =
                    System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        CurrentView.TableName + "_" + Log.ErrorLog.TimeStamp() + ".dat"
                        );

                this.tbFilepath.Text = NewLoadfileFilepath;

                ConcordanceDat Dat = new ConcordanceDat(NewLoadfileFilepath);
                Dat.SetFieldNames(this.CurrentView.FieldNamesAsDisplayed);
                Dat.Save(NewLoadfileFilepath);
                Dat.Close();
                this.Loadfile = Dat;
            }

            ExportWindow Ew = null; 

            try
            {
                Ew = new ExportWindow(this.Loadfile);
                Ew.ShowDialog();
            }
            catch (Exception Ex)
            {
                Status("Error opening Save dialog with loadfile: "
                    + this.Loadfile.FileInformation.Name
                    + Environment.NewLine
                    + Ex.Message
                    );
            }

            if (Ew.DialogResult == true)
            {
                Log.Timer.Start();

                // try to make a backup
                try
                {
                    // make a backup of the loadfile
                    if (this.Loadfile.FileInformation.FullName.ToUpper() == Ew.OutputFilepath.ToUpper())
                    {
                        Status("Creating backup of the original loadfile....");

                        string backupfilename = System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(this.Loadfile.FileInformation.FullName),
                            System.IO.Path.GetFileNameWithoutExtension(this.Loadfile.FileInformation.FullName)
                                + "_LFUBACKUP_"
                                + Log.ErrorLog.TimeStamp()
                                + System.IO.Path.GetExtension(this.Loadfile.FileInformation.FullName)
                            );

                        Log.ErrorLog.AddMessage("Creating backup of " + this.Loadfile.FileInformation.FullName + " as " + backupfilename);

                        // make the copy!
                        File.Copy(this.Loadfile.FileInformation.FullName, backupfilename);
                        Status("Created backup " + backupfilename);
                    }
                }
                catch (Exception Ex)
                {
                    Log.ErrorLog.AddMessage("Error during backup of file: " 
                        + this.Loadfile.FileInformation.FullName 
                        + Environment.NewLine 
                        + Ex.Message 
                        + Environment.NewLine 
                        + Ex.StackTrace);

                    Status("Error during save of file: " + this.Loadfile.FileInformation.FullName);
                }

                // try to save the loadfile
                try
                {
                    // we need to save against a select statement and NOT a tablename -ds 09-23-2015
                    Export.Save(Loadfile, Ew.OutputFilepath, CurrentView.TableName, CurrentView.FieldNamesAsDisplayed, SelectCommandString);

                    Status("Save is complete: " 
                        + Ew.OutputFilepath 
                        + " (" 
                        + Log.Timer.ElapsedTime().ToString() 
                        + ")"
                        );

                    // let's not rename table 
                    // -ds 2015-07-17
                    /*if (Db.Connect.RenameTable(TableName, Db.Connect.GetTableName(Ew.OutputFilepath)) == 1)
                    {
                        // change visible path only if we successfully renamed that backing table
                        this.tbFilepath.Text = Ew.OutputFilepath;
                        TableName = Db.Connect.GetTableName(this.tbFilepath.Text);
                    }*/

                }
                catch (Exception Ex)
                {
                    Log.ErrorLog.AddMessage("Error during save of file: " 
                        + this.Loadfile.FileInformation.FullName 
                        + Environment.NewLine 
                        + Ex.Message 
                        + Environment.NewLine 
                        + Ex.StackTrace);

                    Status("Error during save of file: " + this.Loadfile.FileInformation.FullName);
                }

            }
            else
            {
                Status("Export or Save was cancelled.");
            }

        }


        #endregion




    }
}
