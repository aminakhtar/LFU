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
using Pdc.Loadfiles;

namespace LFU
{

    public delegate void PreviewEventHandler(object sender, PreviewEventArgs preview);

    public class PreviewEventArgs : EventArgs
    {
        public string PreviewString = "";

        public PreviewEventArgs(string previewstring)
        {
            PreviewString = previewstring;
        }
    }

    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public PreviewWindow(string preview)
        {
            InitializeComponent();
            ShowPreview(preview);
        }

        private void ShowPreview(string preview)
        {
            this.tblPreview.Text = preview;
        }

        public void OnPreview(object sender, PreviewEventArgs preview)
        {
            ShowPreview(preview.PreviewString);
        }


    }
}
