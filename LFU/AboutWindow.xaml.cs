using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {

        /// <summary>
        /// The current assembly. From this we get information about the current assembly
        /// such as title, description, copyright information, and version number
        /// </summary>
        private Assembly assembly;

        public AboutWindow()
        {
            InitializeComponent();

            // information about the assembly
            assembly = Assembly.GetEntryAssembly();
            this.tblTitle.Text = GetAttributeValue<AssemblyTitleAttribute>(a => a.Title);
            this.tblDescription.Text = GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description);
            this.tblCopyright.Text = GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright);
            this.tblVersion.Text = "Version "
                + assembly.GetName().Version.ToString();
                /*
                + assembly.GetName().Version.Major.ToString()
                + "." + assembly.GetName().Version.Minor.ToString()
                + "." + assembly.GetName().Version.Build.ToString()
                + "." + assembly.GetName().Version.Revision.ToString();
                */

            // get version of Pdc.Dts that is being used right now
            AssemblyName[] refAssemblies = assembly.GetReferencedAssemblies();

            // find pdc.loadfiles and report version
            foreach (AssemblyName An in refAssemblies)
            {
                if (An.Name.ToLower() == "pdc.loadfiles")
                {
                    this.tblPdcLoadfilesVersion.Text = "Pdc.Loadfiles "
                        + An.Version.ToString();
                    /*
                        + An.Version.Major.ToString()
                        + "." + An.Version.Minor.ToString()
                        + "." + An.Version.Build.ToString()
                        + "." + An.Version.Revision.ToString();
                     */
                }
            }

            // show configured questions email address
            this.tblQuestionsEmailAddress.Text =
                "For questions, comments or to report an issue, contact "
                + ConfigurationManager.AppSettings["SupportEmailAddress"].ToString();

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // show help document
            Process.Start(new ProcessStartInfo(@".\Help\LFU_Documentation.txt"));
        }

        private string GetAttributeValue<TAttr>(Func<TAttr, string> resolveFunc, string defaultResult = null) where TAttr : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(TAttr), false);

            if (attributes.Length > 0)
            {
                return resolveFunc((TAttr)attributes[0]);
            }
            else
            {
                return defaultResult;
            }
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "LFU.exe.config");
        }

    }
}
