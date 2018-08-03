using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SelectHeader.xaml
    /// </summary>
    public partial class SelectHeader : Window
    {

        /// <summary>
        /// Class with a checkbox and a text for keeping the header
        /// </summary>
        public class BoolStringClass
        {
            public string TheText { get; set; }
            public bool IsSelected { get; set; }
        }



        #region "CONSTRUCTOR"

        public SelectHeader(List<string> fieldnames)
        {
            InitializeComponent();

            // TheList is name of our listbox
            TheList = new ObservableCollection<BoolStringClass>();
            Headers = fieldnames;

            // load the list with headers and checkboxes in the first place
            foreach (string item in fieldnames)
            {
                BoolStringClass checkboxobject = new BoolStringClass();
                checkboxobject.IsSelected = true;
                checkboxobject.TheText = item;
                TheList.Add(checkboxobject);
                NumberOfColumns++;
            }

            this.ListBoxHeaders.ItemsSource = TheList;

            // set the selected headers to true
            if (NumberOfColumns != 0)
            {
                SelectedHeaders = new bool[NumberOfColumns];
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    SelectedHeaders[i] = true;
                }
            }

        }

        #endregion



        #region "FIELDS"

        //Collection of boolean checkboxes and the headers
        private ObservableCollection<BoolStringClass> TheList { get; set; }
        private int NumberOfColumns = 0;
        //private bool[] _SelectedHeaders = null;
        private List<string> Headers = new List<string>();
        private bool IsAllSelected = true;
        // public List<string> SelectedFieldNames;

        #endregion



        #region "PROPERTIES"

        public bool[] SelectedHeaders { get; private set; }

        #endregion



        #region "INTERFACE EVENTS"

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            //Acts like switch for turn on/off the selected and unselected
            this.IsAllSelected = !this.IsAllSelected;

            //Delete everything from the list
            TheList.Clear();


            if (IsAllSelected) //If we decide that select all
            {
                //If we select all, the "se selected" button should be enabled
                btnUseSelected.IsEnabled = true;

                // Load the list with headers and set the checkboxes to true                
                for (int i = 0; i < Headers.Count; i++)
                {
                    BoolStringClass checkboxobject = new BoolStringClass();
                    checkboxobject.IsSelected = true;
                    checkboxobject.TheText = Headers[i];
                    TheList.Add(checkboxobject);
                    NumberOfColumns++;
                }

            }
            else //If we decide unselect all
            {
                // Disable the select button
                btnUseSelected.IsEnabled = false;

                // Populate the data and unckeck the boxes
                for (int i = 0; i < Headers.Count; i++)
                {
                    BoolStringClass checkboxobject = new BoolStringClass();
                    checkboxobject.IsSelected = false;
                    checkboxobject.TheText = Headers[i];
                    TheList.Add(checkboxobject);
                    NumberOfColumns++;
                }
            }
        }

        // Press Cancel
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedHeaders = null;
            this.DialogResult = false;
            this.Close();
        }


        private void btnUseSelected_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < TheList.Count; i++)
            {
                SelectedHeaders[i] = TheList[i].IsSelected; // == true ? true : false;
            }

            this.DialogResult = true;
            this.Close();

            //Just for debug
            //global::System.Windows.MessageBox.Show(String.Join(",", SelectedHeaders));
            //The result is in SelectedHeaders
        }

        // Decide about the the "Use_select" button status 
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.btnUseSelected.IsEnabled = false;
            for (int i = 0; i < TheList.Count; i++)
            {
                if (TheList[i].IsSelected == true)
                {

                    this.btnUseSelected.IsEnabled = true;
                }
            }
        }

        #endregion



    }



}
