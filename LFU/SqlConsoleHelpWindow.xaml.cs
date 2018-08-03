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
    /// Interaction logic for SqlConsoleHelpWindow.xaml
    /// </summary>
    public partial class SqlConsoleHelpWindow : Window
    {
        public SqlConsoleHelpWindow()
        {
            InitializeComponent();
            IsHelpShown = true;
        }

        public static bool IsHelpShown = false;

        private void btnWeb_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(
                    "iexplore.exe", 
                    @"http://www.sqlite.org\lang.html")
                );
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IsHelpShown = false;
        }



    }
}
