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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LFU.Db
{
    /// <summary>
    /// Interaction logic for UpdateFieldPairControl.xaml
    /// </summary>
    public partial class UpdateFieldPairControl : UserControl
    {
        public UpdateFieldPairControl(string loadfilefield, string casefield)
        {
            InitializeComponent();
            LoadfileField = loadfilefield;
            CaseField = casefield;
            this.tblLoadfileField.Text = LoadfileField;
            this.tblCaseField.Text = CaseField;
        }

        public string LoadfileField { get; set; }
        public string CaseField { get; set; }

    }
}
