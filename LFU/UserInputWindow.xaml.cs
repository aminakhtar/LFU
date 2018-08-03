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
    /// Interaction logic for UserInputWindow.xaml
    /// </summary>
    public partial class UserInputWindow : Window
    {
        public UserInputWindow(string windowtitle)
        {
            InitializeComponent();
            this.Title = windowtitle;
        }

        /// <summary>
        /// Collection of user's inputs. May be one or more strings.
        /// </summary>
        public IEnumerable<string> Inputs { get; set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.stackInputs.Children.Count > 0)
            {
                Inputs = from UIElement c in this.stackInputs.Children
                         where c is TextBlock
                         select ((TextBlock)c).Text;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                this.tblStatus.Text = "No inputs. Hit enter to queue an input.";
            }
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string NewInputText = this.tbInput.Text.Remove(this.tbInput.Text.Length - 2);
                if (NewInputText.Length > 0)
                {
                    this.tblStatus.Text = "";
                    TextBlock NewInput = new TextBlock();
                    NewInput.Margin = new Thickness(0, 0, 0, 5);
                    NewInput.Text = NewInputText;
                    this.tbInput.Text = "";
                    this.stackInputs.Children.Add(NewInput);
                }
                else
                {
                    this.tblStatus.Text = "Input is empty";
                }
            }
        }

    }
}
