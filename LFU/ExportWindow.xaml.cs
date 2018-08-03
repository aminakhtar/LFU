using System;
using System.Collections.Generic;
using System.IO;
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
    /// <summary>
    /// Interaction logic for ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {

        #region "CONSTRUCTORS"

        public ExportWindow()
        {
            InitializeComponent(); 
        }

        public ExportWindow(LoadfileBase loadfile)
        {
            InitializeComponent();

            this._SelectedLoadfile = loadfile;

            // populate comboboxes, etc
            Init();

            this.tbFilepath.Text = this._SelectedLoadfile.FileInformation.FullName;

        }

        private void Init()
        {
            // populate cmboLoadfileTypes
            this.cmboLoadfileType.ItemsSource = Enum.GetNames(typeof(LoadfileTypes));

            // make a list of encodings
            List<EncodingInfo> enc = new List<EncodingInfo>();

            // collect common encodings at the top of the list
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                if (ei.CodePage == 1252 || ei.CodePage == 20127 || ei.CodePage == 65001 || ei.CodePage == 1200)
                {
                    enc.Add(ei);
                }
            }

            // add the rest of the encodings (including the common ones from above) in order of codepage
            enc.AddRange(Encoding.GetEncodings());
            _Encodings = enc.ToArray<EncodingInfo>();

            // bind cmboEncodings to list of encodings in WPF
            this.cmboEncodings.ItemsSource = _Encodings;
            this.cmboEncodings.DisplayMemberPath = "DisplayName";
            this.cmboEncodings.SelectedValuePath = "CodePage";

            // get delimiters characters
            this.cmboDelimField.ItemsSource = DelimiterCharacters.Get;
            this.cmboDelimText.ItemsSource = DelimiterCharacters.Get;
            this.cmboDelimLineBreakSub.ItemsSource = DelimiterCharacters.Get;

            this.cmboDelimField.DisplayMemberPath = "Display";
            this.cmboDelimText.DisplayMemberPath = "Display";
            this.cmboDelimLineBreakSub.DisplayMemberPath = "Display";

            this.cmboDelimField.SelectedValuePath = "CharValue";
            this.cmboDelimText.SelectedValuePath = "CharValue";
            this.cmboDelimLineBreakSub.SelectedValuePath = "CharValue";

        }

        #endregion



        #region "FIELDS"

        private EncodingInfo[] _Encodings;
        private string _OutputFilepath;
        private LoadfileBase _SelectedLoadfile;

        #endregion



        #region "PROPERTIES"

        /// <summary>
        /// The path to which the loadfile will be saved or exported
        /// </summary>
        public string OutputFilepath
        {
            get
            {
                return _OutputFilepath;
            }

            private set
            {
                _OutputFilepath = value;
                this.Title = "Export Loadfile - " + System.IO.Path.GetFileName(_OutputFilepath);
            }
        }

        public LoadfileBase SelectedLoadfile
        {
            get
            {
                return this._SelectedLoadfile;
            }
        }

        #endregion



        #region "METHOD"

        private void ShowLoadfileSettings()
        {
            // Loadfile Type
            // select in the combobox the loadfile type that is the type of the loadfile object sent to this dialog
            this.cmboLoadfileType.SelectedValue = this._SelectedLoadfile.LoadfileType.ToString();

            // Encoding
            // show the controls if appropriate
            if (this._SelectedLoadfile is IPlainText)
            {
                this.wpanelOutputEncoding.Visibility = System.Windows.Visibility.Visible;
                this.tblLoadfileCurrentEncoding.Text = ((IPlainText)this._SelectedLoadfile).FileEncoding.EncodingName;
                this.cmboEncodings.SelectedValue = ((IPlainText)this._SelectedLoadfile).FileEncoding.CodePage;
            }
            else
            {
                this.wpanelOutputEncoding.Visibility = System.Windows.Visibility.Collapsed;
            }

            // Delimiters and IncludeHeader
            // show the controls if appropriate
            if (this._SelectedLoadfile is ConcordanceDat || this._SelectedLoadfile is Csv)
            {
                this.wpanelDelimiters.Visibility = System.Windows.Visibility.Visible;            
                this.cmboDelimField.SelectedValue = ((IDelimited)this._SelectedLoadfile).FieldDelimiter[0]; 
                this.cmboDelimText.SelectedValue = ((IDelimited)this._SelectedLoadfile).TextDelimiter[0];

                this.wpanelIncludeHeaders.Visibility = System.Windows.Visibility.Visible;
                this.chkIncludeHeaders.IsChecked = ((IDelimited)this._SelectedLoadfile).HasHeader;

                if (_SelectedLoadfile is ConcordanceDat)
                {
                    this.cmboDelimLineBreakSub.SelectedValue = ((ConcordanceDat)this._SelectedLoadfile).LineBreakSubstitute[0];
                }
            }
            else
            {
                this.wpanelDelimiters.Visibility = System.Windows.Visibility.Collapsed;
                this.wpanelIncludeHeaders.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        #endregion



        #region "INTERFACE EVENTS"

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            string initialDirectory = null;

            if (File.Exists(this.tbFilepath.Text) || Directory.Exists(this.tbFilepath.Text))
            {
                initialDirectory = System.IO.Path.GetDirectoryName(this.tbFilepath.Text);
            }

            Microsoft.Win32.SaveFileDialog Sfd = new Microsoft.Win32.SaveFileDialog();

            Sfd.DefaultExt = ".dat";
            Sfd.OverwritePrompt = true;
            Sfd.Filter = "Concordance DAT|*.dat|Opticon|*.opt; *.log|Ipro LFP|*.lfp|Comma Separated Values|*.csv; *.tsv|Summation|*.dii|All|*.*";
            Sfd.RestoreDirectory = true;
            Sfd.InitialDirectory = initialDirectory;

            if (Sfd.ShowDialog() == true)
            {
                this.tbFilepath.Text = Sfd.FileName;
            }
        }

        private void tbFilepath_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.OutputFilepath = this.tbFilepath.Text;
            ShowLoadfileSettings();
        }

        private void chkIncludeHeaders_Checked(object sender, RoutedEventArgs e)
        {
            ((IDelimited)_SelectedLoadfile).HasHeader = true;
        }

        private void chkIncludeHeaders_Unchecked(object sender, RoutedEventArgs e)
        {
            ((IDelimited)_SelectedLoadfile).HasHeader = false;
        }

        private void cmboEncodings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmboEncodings.SelectedValue != null)
            {
                ((IPlainText)this._SelectedLoadfile).FileEncoding = Encoding.GetEncoding((int)this.cmboEncodings.SelectedValue);
            }
        }

        private void cmboDelimField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmboDelimField.SelectedValue != null)
            {
                ((IDelimited)_SelectedLoadfile).FieldDelimiter = this.cmboDelimField.SelectedValue.ToString();
            }
        }

        private void cmboDelimText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmboDelimText.SelectedValue != null)
            {
                ((IDelimited)_SelectedLoadfile).TextDelimiter = this.cmboDelimText.SelectedValue.ToString();
            }
        }

        private void cmboDelimLineBreakSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmboDelimLineBreakSub.SelectedValue != null)
            {
                ((ConcordanceDat)_SelectedLoadfile).LineBreakSubstitute = this.cmboDelimLineBreakSub.SelectedValue.ToString();
            }
        }


        #endregion




    }
}
