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

namespace LFU
{
    /// <summary>
    /// Interaction logic for MergeWindow.xaml
    /// </summary>
    public partial class MergeWindow : Window
    {
        List<string> ListOfAllFields;

        public MergeWindow(List<string> listofallfields)
        {
            InitializeComponent();
            ListOfAllFields = listofallfields;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cmbFirst.ItemsSource = ListOfAllFields;
            this.cmbSecond.ItemsSource = ListOfAllFields;
            this.cmbResult.ItemsSource = ListOfAllFields;
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
