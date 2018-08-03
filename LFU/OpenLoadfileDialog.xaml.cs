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
    /// Interaction logic for OpenLoadfileDialog.xaml
    /// </summary>
    public partial class OpenLoadfileDialog : Window
    {

        #region "CONSTRUCTORS"

        public OpenLoadfileDialog()
        {
            InitializeComponent();
            Init();
        }

        public OpenLoadfileDialog(string filepath)
        {
            InitializeComponent();
            Init();

            if (File.Exists(filepath))
            {
                this.tbFilepath.Text = filepath;
            }

        }

        private void Init()
        {
            // make a list of encodings
            List<EncodingInfo> enc = new List<EncodingInfo>();

            // collect common encodings at the top of the list
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                if (ei.CodePage == 1252 || ei.CodePage == 20127 || ei.CodePage == 65001 || ei.CodePage == 1200 )
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

            // wait till we get a loadfile object to set the values on the form
            //this.cmboEncodings.SelectedValue = 1252;

            this.cmboDelimField.ItemsSource = DelimiterCharacters.Get;
            this.cmboDelimText.ItemsSource = DelimiterCharacters.Get;
            this.cmboDelimLineBreakSub.ItemsSource = DelimiterCharacters.Get;

            this.cmboDelimField.DisplayMemberPath = "Display";
            this.cmboDelimText.DisplayMemberPath = "Display";
            this.cmboDelimLineBreakSub.DisplayMemberPath = "Display";

            this.cmboDelimField.SelectedValuePath = "CharValue";
            this.cmboDelimText.SelectedValuePath = "CharValue";
            this.cmboDelimLineBreakSub.SelectedValuePath = "CharValue";

            // wait till we get a loadfile object to set the values on the form
            //this.cmboDelimField.SelectedValue = (char)20;
            //this.cmboDelimText.SelectedValue = (char)254;
            //this.cmboDelimLineBreakSub.SelectedValue = (char)174;

        }

        #endregion



        #region "FIELDS"

        private EncodingInfo[] _Encodings;
        private LoadfileBase _SelectedLoadfile;
        public event PreviewEventHandler ShowPreview;
        private PreviewWindow Pw;

        #endregion



        #region "PROPERTIES"

        public LoadfileBase SelectedLoadfile
        {
            get
            {
                if (_SelectedLoadfile == null)
                {
                    return null;
                }
                else
                {
                    return _SelectedLoadfile;
                }
            }
        }

        #endregion



        #region "METHODS"

        private void OpenPreview()
        {
            Log.Timer.Start();
            this.tblStatus.Text = "Examining " + _SelectedLoadfile.FileInformation.Name;

            _SelectedLoadfile.Open(
                this.chkAutoDetectEncoding.IsChecked == true 
                ? true 
                : false
                );
            
            // show the encoding in the loadfile on the form
            if (_SelectedLoadfile is IPlainText)
            {
                this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
            }

            this.tblFieldNames.Text = string.Join(", ", _SelectedLoadfile.FieldNames);
            this.tblStatus.Text = this.tblStatus.Text + " (" + Log.Timer.ElapsedTime() + ")";
        }

        #endregion



        #region "USER INTERFACE EVENTS"

        private void tbFilepath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(this.tbFilepath.Text))
            {
                _SelectedLoadfile = LoadfileFactory.Build(this.tbFilepath.Text);
                this.tblLoadfileType.Text = _SelectedLoadfile.LoadfileType.ToString();
                this.tblStatus.Text = "Examining " + _SelectedLoadfile.FileInformation.Name;
                this.Title = "Open Loadfile - " + _SelectedLoadfile.FileInformation.Name;
                this.stackFieldPreview.Visibility = System.Windows.Visibility.Visible;

                
                if (_SelectedLoadfile is IPlainText)
                {
                    this.stackEncoding.Visibility = Visibility.Visible;
                    this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                }

                if (_SelectedLoadfile is ConcordanceDat || _SelectedLoadfile is Csv)
                {
                    this.cmboDelimField.SelectedValue = ((IDelimited)_SelectedLoadfile).FieldDelimiter[0];
                    this.cmboDelimText.SelectedValue = ((IDelimited)_SelectedLoadfile).TextDelimiter[0];
                }

                switch (_SelectedLoadfile.LoadfileType)
                {
                    case LoadfileTypes.ConcordanceDat:
                        this.wrapDelimiters.Visibility = Visibility.Visible;
                        this.cmboDelimLineBreakSub.IsEnabled = true;
                        this.cmboDelimLineBreakSub.SelectedValue = ((ConcordanceDat)_SelectedLoadfile).LineBreakSubstitute[0];
                        this.cmboDelimField.SelectedValue = ((ConcordanceDat)_SelectedLoadfile).FieldDelimiter[0];
                        this.cmboDelimText.SelectedValue = ((ConcordanceDat)_SelectedLoadfile).TextDelimiter[0];
                        this.stackHasHeaders.Visibility = Visibility.Visible;
                        this.chkHasHeaders.IsChecked = ((ConcordanceDat)_SelectedLoadfile).HasHeader;
                        //this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                        break;

                    case LoadfileTypes.Opticon:
                        this.cmboDelimLineBreakSub.IsEnabled = false;
                        this.wrapDelimiters.Visibility = Visibility.Collapsed;
                        this.stackHasHeaders.Visibility = Visibility.Collapsed;
                        this.chkHasHeaders.IsChecked = false;
                        //this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                        break;

                    case LoadfileTypes.Csv:
                        this.cmboDelimLineBreakSub.IsEnabled = false;
                        this.wrapDelimiters.Visibility = Visibility.Visible;
                        this.stackHasHeaders.Visibility = Visibility.Visible;
                        this.chkHasHeaders.IsChecked = ((Csv)_SelectedLoadfile).HasHeader;
                        //this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                        break;

                    case LoadfileTypes.IproLfp:
                        this.cmboDelimLineBreakSub.IsEnabled = false;
                        this.wrapDelimiters.Visibility = Visibility.Collapsed;
                        this.stackHasHeaders.Visibility = Visibility.Collapsed;
                        this.chkHasHeaders.IsChecked = false;
                        //this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                        break;

                    case LoadfileTypes.SummationDii:
                        this.wrapDelimiters.Visibility = Visibility.Collapsed;
                        this.stackHasHeaders.Visibility = Visibility.Collapsed;
                        this.chkHasHeaders.IsChecked = false;
                        //this.cmboEncodings.SelectedValue = ((IPlainText)_SelectedLoadfile).FileEncoding.CodePage;
                        break;
                }

                // load list of fields AFTER we've set the default encoding and delimiters
                OpenPreview();

            }
            else
            {
                this.tblStatus.Text = "File \"" + this.tbFilepath.Text + "\" does not exist!";
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog Ofd = new Microsoft.Win32.OpenFileDialog();

            Ofd.DefaultExt = ".dat";
            Ofd.Filter = "All|*.*|Concordance DAT|*.dat|Opticon|*.opt; *.log|Ipro LFP|*.lfp|Comma Separated Values|*.csv; *.tsv|Summation|*.dii";
            Ofd.RestoreDirectory = true;

            if (Ofd.ShowDialog() == true)
            {
                this.tbFilepath.Text = Ofd.FileName;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.tblStatus.Text = "Cancelling....";
            _SelectedLoadfile = null;
            this.DialogResult = false;
            this.Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(this.tbFilepath.Text))
            {
                if (_SelectedLoadfile != null)
                {
                    this.tblStatus.Text = "Loading " + _SelectedLoadfile.FileInformation.Name;
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    this.tblStatus.Text = "No loadfile is selected";
                }
            }
            else
            {
                this.tblStatus.Text = "File does not exist";
            }
        }

        private void btnSelectFields_Click(object sender, RoutedEventArgs e)
        {

            if (_SelectedLoadfile != null)
            {
                _SelectedLoadfile.Open(this.chkAutoDetectEncoding.IsChecked == true ? true : false);
                SelectHeader Shd = new SelectHeader(_SelectedLoadfile.FieldNames);

                if (Shd.ShowDialog() == true)
                {
                    this._SelectedLoadfile.FieldsSelected = Shd.SelectedHeaders;
                }

            }
            else
            {
                this.tblStatus.Text = "No loadfile is selected.";
            }

        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            if (this._SelectedLoadfile != null)
            {
                if (_SelectedLoadfile is IDelimited || _SelectedLoadfile is IPlainText)
                {
                    // get the encoding selected on the form
                    Encoding PreviewEnc = Encoding.GetEncoding((int)this.cmboEncodings.SelectedValue);
                    Pw = new PreviewWindow(_SelectedLoadfile.Preview(11, PreviewEnc));
                    Pw.Show();
                    ShowPreview += Pw.OnPreview;
                }
                else
                {
                    this.tblStatus.Text = "No preview for this loadfile type: " + _SelectedLoadfile.GetType().ToString();
                }
            }
            else
            {
                this.tblStatus.Text = "Please, open a loadfile first.";
            }

            // later we might want to return an encoding from the Preview 
            // we'll add controls to tweak the encoding and delimiters 
            // so the user can see live changes and decide accordingly.

        }

        private void cmboEncodings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Encoding
            if (this._SelectedLoadfile is IPlainText)
            {
                if (this.cmboEncodings.SelectedValue != null)
                {
                    ((IPlainText)this._SelectedLoadfile).FileEncoding = Encoding.GetEncoding(
                            (int)this.cmboEncodings.SelectedValue
                        );

                    // need to re-open to get fields with proper encoding
                    OpenPreview();

                    if (ShowPreview != null)
                    {
                        ShowPreview(this, new PreviewEventArgs(_SelectedLoadfile.Preview(30, ((IPlainText)this._SelectedLoadfile).FileEncoding)));
                    }
                }
            }
        }

        private void cmboDelimField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Delimiters
            if (this._SelectedLoadfile is ConcordanceDat || this._SelectedLoadfile is Csv)
            {
                // Field Delimiter
                if (this.cmboDelimField.SelectedValue != null)
                {
                    ((IDelimited)this._SelectedLoadfile).FieldDelimiter = this.cmboDelimField.SelectedValue.ToString();
                    OpenPreview();
                }
            }
        }

        private void cmboDelimText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._SelectedLoadfile is ConcordanceDat || this._SelectedLoadfile is Csv)
            {
                // Text Delimiter
                if (this.cmboDelimText.SelectedValue != null)
                {
                    ((IDelimited)this._SelectedLoadfile).TextDelimiter = this.cmboDelimText.SelectedValue.ToString();
                    OpenPreview();

                }
            }
        }

        private void cmboDelimLineBreakSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Concordance Linebreak Substitution character
            if (this._SelectedLoadfile is ConcordanceDat)
            {
                if (this.cmboDelimLineBreakSub.SelectedValue != null)
                {
                    ((ConcordanceDat)this._SelectedLoadfile).LineBreakSubstitute = this.cmboDelimLineBreakSub.SelectedValue.ToString();
                    OpenPreview();
                }
            }
        }

        private void chkHasHeaders_Checked(object sender, RoutedEventArgs e)
        {
            chkHasHeaders_CheckChanged();
        }

        private void chkHasHeaders_Unchecked(object sender, RoutedEventArgs e)
        {
            chkHasHeaders_CheckChanged();
        }

        private void chkHasHeaders_CheckChanged()
        {
            if (this._SelectedLoadfile is IDelimited)
            {
                ((IDelimited)this._SelectedLoadfile).HasHeader = this.chkHasHeaders.IsChecked == true ? true : false;

                // need to re-open to get fields with proper encoding
                OpenPreview();
            }
        }

        private void chkAutoDetectEncoding_Checked(object sender, RoutedEventArgs e)
        {
            if (this.cmboEncodings != null)
            {
                this.cmboEncodings.IsEnabled = false;
            }
        }

        private void chkAutoDetectEncoding_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.cmboEncodings != null)
            {
                this.cmboEncodings.IsEnabled = true;
            }
        }


        #endregion


    }
}
