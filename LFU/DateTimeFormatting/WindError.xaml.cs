using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindError.xaml
    /// </summary>
    public partial class WindError : Window
    {
        public WindError(List<string> ErrorList)
        {
            InitializeComponent();
            errorlist = ErrorList;
        }

        List<string> errorlist;

        private void lstError_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string item in errorlist)
            {
                this.lstError.Items.Add(item);
            }
            
        }

        private void lblNumber_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblNumber.Content = errorlist.Count;
        }
    }
}
