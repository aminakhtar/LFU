using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using LFU.Aqc;
using LFU.DateTimeFormatting;
using LFU.SortField;

namespace LFU.Views
{

    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page, IViewPage
    {
        public TablePage(GridViewLoadfile gridview, LoadfileBase loadfile)
        {
            InitializeComponent();

            Dgv = gridview;

            // Collect the fieldnames added to the grid
            this.cmboFunctionField.ItemsSource = Dgv.FieldNamesAsDisplayed;
            this.cmboFunctionField.DataContext = Dgv;

            this.tbPageRowCount.Text = Dgv.PageRowCount.ToString();
            this.tbLastPage.Text = Dgv.LastPage.ToString();
            this.tblTotalRowCount.Text = Dgv.TotalRowCount.ToString("#,##0") + " rows";

            // wire up the paging events now that we are ready for the user
            this.tbLastPage.TextChanged += tbLastPage_TextChanged;
            this.tbCurrentPage.TextChanged += tbCurrentPage_TextChanged;

            Dgv.Sort(this.dgData, 0);

            // the loadfile object
            this.Loadfile = loadfile;

        }

        #region "PROPERTIES"

        public event Log.StatusEventHandler StatusUpdate;

        private GridViewLoadfile Dgv;
        private LoadfileBase Loadfile;

        public string TabName { get; set; }

        private string _TableName;
        public string TableName
        {
            get
            {
                return Dgv.TableName;
            }

            set
            {
                _TableName = value;
            }

        }

        private List<string> _FieldNamesAsDisplayed;
        public List<string> FieldNamesAsDisplayed
        {
            get
            {
                return Dgv.FieldNamesAsDisplayed;
            }

            set
            {
                _FieldNamesAsDisplayed = value;
            }
        }

        #endregion


        #region "USER INTERFACE EVENTS"

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Setting of the form when it loads
            this.tbCurrentPage.IsEnabled = true;
            this.btnFilter.IsEnabled = true;
            this.btnIndex.IsEnabled = true;
            this.btnPrevPage.IsEnabled = true;
            this.btnNextPage.IsEnabled = true;
            this.btnSort.IsEnabled = true;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Db.LawImportExport importwindow = new Db.LawImportExport(this.TableName, this.Dgv.FieldNamesAsDisplayed);
                if (importwindow.ShowDialog() == true)
                {
                    Status(@"LAW import\export data complete. (" + importwindow.UpdateCount.ToString("#,##0") + " updates)");
                }
                else if (importwindow.DialogResult == false)
                {
                    Status(@"LAW import\export data cancelled or failed.");
                }
            }catch(Exception ex){
                Status("Import:" + ex.Message);
            }
        }

        private void btnAqc_Click(object sender, RoutedEventArgs e)
        {
            AqcUi AqcUiWindow = new AqcUi(this.Loadfile, TableName);
            AqcUiWindow.ShowDialog();
        }

        #endregion


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


        #region "PAGING INTERFACE EVENTS"

        private void tbLastPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dgv.LastPage = Convert.ToInt32(this.tbLastPage.Text);
        }

        private void tbCurrentPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.tbCurrentPage.IsFocused)
            {
                ChangeCurrentPage();
            }
        }

        private void ChangeCurrentPage()
        {
            try
            {
                if (this.cmboFunctionField != null)
                {
                    Log.Timer.Start();

                    Dgv.CurrentPage = Convert.ToInt32(this.tbCurrentPage.Text); 

                    if (Dgv.CurrentPage > Dgv.LastPage)
                    {
                        Dgv.CurrentPage = Dgv.LastPage;
                        this.tbLastPage.Text = Dgv.LastPage.ToString();
                    }
                    else if (Dgv.CurrentPage < 1)
                    {
                        Dgv.CurrentPage = 1;
                        this.tbCurrentPage.Text = "1";
                    }

                    // table starts with row 1 not row 0
                    if (string.IsNullOrWhiteSpace(Dgv.OrderByClause))
                    {
                        // no current sortstate, sort on default which is rowid asc
                        Dgv.Sort(this.dgData, Dgv.PageRowCount * (Dgv.CurrentPage - 1));
                    }
                    else
                    {
                        // use the user's SortState
                        Dgv.Sort(this.dgData, Dgv.PageRowCount * (Dgv.CurrentPage - 1));
                    }

                    Status("Jumped to page " 
                        + Dgv.CurrentPage.ToString("#,##0") 
                        + " in "                         
                        + Log.Timer.ElapsedTime().ToString());
                }
            }
            catch (Exception Ex)
            {
                this.tbCurrentPage.Text = Dgv.CurrentPage.ToString();
                Status(Ex.Message);
                return;
            }
        }

        private void tbCurrentPage_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeCurrentPage();
        }

        // Next Page button
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            int currentpage = Convert.ToInt32(this.tbCurrentPage.Text);
            if (currentpage < Dgv.LastPage)
            {
                this.tbCurrentPage.Text = (currentpage + 1).ToString();
            }
        }

        // Previous Page button
        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            int currentpage = Convert.ToInt32(this.tbCurrentPage.Text);
            if (currentpage > 1)
            {
                //Update the current page
                this.tbCurrentPage.Text = (currentpage - 1).ToString();
            }
        }

        // Wait for the user to move off the page size before we start to recalc
        private void tbPageRowCount_LostFocus(object sender, RoutedEventArgs e)
        {
            PageSizeChanged();
        }

        /// <summary>
        /// Recalculate pages
        /// </summary>
        private void PageSizeChanged()
        {
            try
            {
                // Set the page size
                int temp = Convert.ToInt32(this.tbPageRowCount.Text);

                if (temp > Dgv.TotalRowCount)
                {
                    temp = Dgv.TotalRowCount;
                }

                if (temp > 0 && temp <= Dgv.TotalRowCount)
                {
                    Dgv.PageRowCount = temp;
                    this.tbPageRowCount.Text = Dgv.PageRowCount.ToString();
                    Dgv.LastPage = (Dgv.TotalRowCount / Dgv.PageRowCount) + 1;

                    if (Dgv.CurrentPage > Dgv.LastPage)
                    {
                        Dgv.CurrentPage = Dgv.LastPage;
                    }

                    // use System.Math.Ceiling to determine LastPage
                    this.tbLastPage.Text = Dgv.LastPage.ToString();

                    // if we change the page size then we go back to CurrentPage
                    // this will force the gridview to refresh at the CurrentPage
                    this.tbCurrentPage.Text = "";
                }
            }
            catch (Exception Ex)
            {
                this.tbPageRowCount.Text = Dgv.PageRowCount.ToString();
                Status(Ex.Message);
                return;
            }
        }

        #endregion


        #region "PROCESS INTERFACE EVENTS"

        /// <summary>
        /// Drop a column from the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDropColumn_Click(object sender, RoutedEventArgs e)
        {
            string SelectedColumnName = this.cmboFunctionField.SelectedItem.ToString();

            Db.Connect.DropColumn(Dgv.TableName, SelectedColumnName);
            Dgv.FieldNamesAsDisplayed.Remove(SelectedColumnName);

            // until we undersand binding a little better....
            this.cmboFunctionField.ItemsSource = null;
            this.cmboFunctionField.ItemsSource = Dgv.FieldNamesAsDisplayed;

            // we don't need to alter the combobox ourselves because it is bound to FieldNamesAsDisplayed
            // cmboFunctionField.Items.Remove(cmboFunctionField.SelectedItem.ToString());

            // reset the selected item in our combobox now that the formerly selected item has been removed
            cmboFunctionField.SelectedIndex = 0;

            // call sort to refresh the view
            Dgv.Sort(this.dgData, 0);

            Status("Dropped column " + SelectedColumnName + " from table " + Dgv.TableName);
        }

        /// <summary>
        /// Add a column or columnd to the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            UserInputWindow Uiw = new UserInputWindow("Enter column names");

            if (Uiw.ShowDialog() == true)
            {
                Log.Timer.Start();

                // get one or more strings from our generic user input dialog
                List<string> NewColumnNames = new List<string>(Uiw.Inputs);

                // call sqlite to alter table add column
                Db.Connect.AddColumns(this.TableName, NewColumnNames);

                // update the field names as displayed
                // the combobox of field names is bound to this list
                Dgv.FieldNamesAsDisplayed.AddRange(NewColumnNames);

                // until we undersand binding a little better....
                this.cmboFunctionField.ItemsSource = null;
                this.cmboFunctionField.ItemsSource = Dgv.FieldNamesAsDisplayed;

                this.cmboFunctionField.SelectedIndex = 0;
                // call sort to refresh the gridview
                this.Dgv.Sort(this.dgData, 0);

                Status("Added column" 
                    + (NewColumnNames.Count == 1 ? "s " : " ") 
                    + string.Join(", ", NewColumnNames) 
                    + " to table " 
                    + Dgv.TableName
                    + ". (" + Log.Timer.ElapsedTime().ToString()
                    + ")"
                    );
            }
        }

        /// <summary>
        /// Sort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            //Set the NumberOfRows with page size
            Dgv.PageRowCount = Convert.ToInt32(tbPageRowCount.Text);

            //After sort we want to start from the first page again
            Log.Timer.Start();
            string sortfield = cmboFunctionField.SelectedItem.ToString();
            Dgv.OrderByClause = sortfield; // fix this!
            Dgv.Sort(this.dgData, 0);

            Status("Sorted on column "
                + cmboFunctionField.SelectedItem.ToString()
                + " ("
                + Log.Timer.ElapsedTime().ToString()
                + ")");

        }

        /// <summary>
        /// Filter view to a user-provided search term
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Log.Timer.Start();
            int NumberofHits = Dgv.Search(this.dgData, cmboFunctionField.SelectedItem.ToString(), tbSearchTerm.Text);
            this.HitsResultLabel.Text = NumberofHits.ToString("#,##0") + " hits";

            Status("Filtered on \""
                + this.tbSearchTerm.Text
                + "\" on column "
                + cmboFunctionField.SelectedItem.ToString()
                + " ("
                + Log.Timer.ElapsedTime().ToString()
                + ")");
        }

        private void tbSearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbSearchTerm.Text)) 
            {
                this.btnFilter.IsEnabled = true;
            } 
            else 
            {
                this.btnFilter.IsEnabled = false;
            }
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Timer.Start();
                Db.Connect.ApplyIndex(Dgv.TableName, cmboFunctionField.SelectedItem.ToString());

                Status("Index created on "
                    + cmboFunctionField.SelectedItem.ToString()
                    + " (" + Log.Timer.ElapsedTime().ToString()
                    + ")");
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage(
                    "Failed to create an index on " 
                    + Db.Connect.DatabasePath 
                    + "." 
                    + Dgv.TableName 
                    + Environment.NewLine 
                    + Ex.Message 
                    + Environment.NewLine 
                    + Ex.StackTrace
                    );
            }

        }

        #endregion

        
        #region "GRID INTERFACE EVENTS"

        /// <summary>
        /// Update a single cell. Not suitable for mass editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string NewValue = ((TextBox)e.EditingElement).Text;
            string UpdatedHeader = e.Column.Header.ToString();

            //It finds the ID of each row which is changed
            DataRowView dataRow = (DataRowView)this.dgData.SelectedItem;

            int UpdatedRow = Convert.ToInt32(dataRow["rowid"]);

            Dgv.Update(NewValue, UpdatedRow, UpdatedHeader);

        }

        /// <summary>
        /// When columns are reordered in the View, make a list of the field names in the order they appear on the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDataGrid_ColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            this.dgData.UpdateLayout();
            Dgv.FieldNamesAsDisplayed.Clear();

            for (int i = 0; i < this.dgData.Columns.Count; i++)
            {
                DataGridColumn Dgc = this.dgData.ColumnFromDisplayIndex(i);
                if (Dgc.Visibility == System.Windows.Visibility.Visible)
                {
                    Dgv.FieldNamesAsDisplayed.Add(Dgc.Header.ToString());
                }
            }            
        }





        #endregion

        
        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            Status("Merging the fields started...please wait.");
            MergeWindow Mgwind = new MergeWindow(Dgv.FieldNamesAsDisplayed);
            Mgwind.ShowDialog();

            if(Mgwind.DialogResult == true)
            {
                Db.Connect.MergeColumns(this.TableName, Mgwind.cmbFirst.SelectedItem.ToString(), Mgwind.txtBetween.Text.ToString(), Mgwind.cmbSecond.SelectedItem.ToString(),Mgwind.cmbResult.SelectedItem.ToString());

            }
            Status("Merging is done...please refresh your window.");
        }

        private void btnDateTime_Click(object sender, RoutedEventArgs e)
        {
            Status("Converting the date and time started...please wait.");
            ConvertDateTime windDateTime = new ConvertDateTime(Dgv.FieldNamesAsDisplayed);
            windDateTime.ShowDialog();

            if (windDateTime.DialogResult == true)
            {
               List<string> ErrorList = Db.Connect.UpdateDateTime(this.TableName,
                    windDateTime.cmbInputColumn.SelectedItem.ToString(),
                    windDateTime.cmbOutputColumn.SelectedItem.ToString(),
                    windDateTime.cmbInputFormat.SelectedItem.ToString(),
                    windDateTime.cmbOutputFormat.SelectedItem.ToString());

                if(ErrorList.Count != 0)
                {
                    WindError WE = new WindError(ErrorList);
                    WE.ShowDialog();
                }
            }
            Status("Converting the date and time is done.please refresh your window.");
        }

        private void btnFieldSort_Click(object sender, RoutedEventArgs e)
        {
            windSortingFields windSrtField = new windSortingFields(Dgv);
            windSrtField.ShowDialog();
        }
    }
}
