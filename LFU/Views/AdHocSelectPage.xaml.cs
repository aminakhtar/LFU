using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Pdc.Loadfiles;

namespace LFU.Views
{
    /// <summary>
    /// Interaction logic for AdHocSelectPage.xaml
    /// </summary>
    public partial class AdHocSelectPage : Page, IViewPage
    {
        public AdHocSelectPage(GridViewSelect gridview)
        {
            InitializeComponent();

            Dgv = gridview;

            this.tbPageRowCount.Text = Dgv.PageRowCount.ToString();
            this.tbLastPage.Text = Dgv.LastPage.ToString();
            this.tblTotalRowCount.Text = Dgv.TotalRowCount.ToString("#,##0") + " rows";

            // wire up the paging events now that we are ready for the user
            this.tbLastPage.TextChanged += tbLastPage_TextChanged;
            this.tbCurrentPage.TextChanged += tbCurrentPage_TextChanged;

            Dgv.Sort(this.dgData, 0);

            // this.Loadfile = new ConcordanceDat();

        }

        #region "FIELDS AND PROPERTIES"

        private GridViewSelect Dgv;
        public event Log.StatusEventHandler StatusUpdate;

        /// <summary>
        /// We may need this to help define and save the ad hoc select to a well-formed loadfile
        /// </summary>
        public LoadfileBase Loadfile;

        public string _TabName;
        public string TabName
        {
            get
            {
                return "Ad Hoc Select";
            }

            set
            {
                _TabName = value;
            }
        }

        public string TableName
        {
            get;
            set;
        }

        private List<string> _FieldNamesAsDisplayed;
        public List<string> FieldNamesAsDisplayed 
        {
            get
            {
                var columnNames = from dgc in this.dgData.Columns
                                  select dgc.Header.ToString();
                return columnNames.ToList<string>();
            }

            set
            {
                _FieldNamesAsDisplayed = value;
            }
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

        #region "PAGING CONTROLS"

        private void ChangeCurrentPage()
        {
            try
            {
                Dgv.CurrentPage = Convert.ToInt32(this.tbCurrentPage.Text);

                if (Dgv.CurrentPage > Dgv.LastPage)
                {
                    Dgv.CurrentPage = Dgv.LastPage;
                }
                else if (Dgv.CurrentPage < 1)
                {
                    Dgv.CurrentPage = 1;
                    this.tbCurrentPage.Text = "1";
                }

                Log.Timer.Start();

                // table starts with row 1 not row 0
                Dgv.Sort(this.dgData, Dgv.PageRowCount * (Dgv.CurrentPage - 1));

                Status("Jumped to page " 
                    + Dgv.CurrentPage.ToString("#,##0") 
                    + " in " 
                    + Log.Timer.ElapsedTime().ToString()
                    );
            }
            catch (Exception Ex)
            {
                this.tbCurrentPage.Text = Dgv.CurrentPage.ToString();
                Status(Ex.Message);
                return;
            }
        }

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

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            int currentpage = Convert.ToInt32(this.tbCurrentPage.Text);
            if (currentpage > 1)
            {
                this.tbCurrentPage.Text = (currentpage - 1).ToString();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            int currentpage = Convert.ToInt32(this.tbCurrentPage.Text);
            if (currentpage < Dgv.LastPage)
            {
                this.tbCurrentPage.Text = (currentpage + 1).ToString();
            }
        }

        private void tbCurrentPage_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeCurrentPage();
        }

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
                //Set the page size
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

                Status("Error when page size changed. " 
                    + Environment.NewLine 
                    + Ex.Message 
                    + Environment.NewLine 
                    + Ex.StackTrace
                    );

                return;
            }
        }

        #endregion

    }
}
