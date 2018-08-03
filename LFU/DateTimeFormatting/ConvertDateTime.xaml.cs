using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace LFU.DateTimeFormatting
{
    /// <summary>
    /// Interaction logic for ConvertDateTime.xaml
    /// </summary>
    public partial class ConvertDateTime : Window
    {
        List<string> listcolumns;

        public ConvertDateTime(List<string> ListOfColumns)
        {
            InitializeComponent();

            listcolumns = ListOfColumns;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cmbInputColumn.ItemsSource = listcolumns;
            this.cmbOutputColumn.ItemsSource = listcolumns;


            List<string> InputFormatList = new List<string>();
            
            #region old
            //InputFormatList.Add("M/d/yyyy");
            //InputFormatList.Add("MM/dd/yyyy");
            //InputFormatList.Add("dd/MM/yyyy");
            //InputFormatList.Add("yyyy/MM/dd");
            //InputFormatList.Add("yyyy/dd/MM");
            //InputFormatList.Add("MM/dd/yyyy h:mm tt");
            //InputFormatList.Add("M/d/yyyy h:mm");
            //InputFormatList.Add("dd/MM/yyyy HH:mm");

            //InputFormatList.Add("MMddyyyy");
            //InputFormatList.Add("yyyyddMM");
            //InputFormatList.Add("yyyyMMdd");
            #endregion

            // Get the list of formats from the App.config
            string DateFormatInput = ConfigurationManager.AppSettings["InputDateFormats"].ToString();
            InputFormatList = DateFormatInput.Split(';').ToList<string>();

            this.cmbInputFormat.ItemsSource = InputFormatList;

            List<string> OutFormatList = new List<string>();
            OutFormatList.Add("MM/dd/yyyy");
            OutFormatList.Add("h:m tt");
            OutFormatList.Add("M/d/yyyy h:mm tt");
            OutFormatList.Add("MM/dd/yyyy hh:mm tt");
            OutFormatList.Add("MM-dd-yyyy");
            OutFormatList.Add("hh:mm:ss");
            OutFormatList.Add("yyyyddmm");
            OutFormatList.Add("MM-dd-yy");
            OutFormatList.Add("MM/dd/yyyy hh:mm");
            OutFormatList.Add("dd/MM/yyyy HH:mm");




            this.cmbOutputFormat.ItemsSource = OutFormatList;


        }
    }
}
