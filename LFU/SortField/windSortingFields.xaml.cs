using LFU.Views;
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

namespace LFU.SortField
{
    /// <summary>
    /// Interaction logic for windSortingFields.xaml
    /// </summary>
    public partial class windSortingFields : Window
    {
        GridViewLoadfile Dgv;

        public windSortingFields(GridViewLoadfile gridview)
        {
            InitializeComponent();
            Dgv = gridview;
        }

        private void StackPanel_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void StackPanel_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            this.lblFilePath.Content = "";
            this.lblNumberOfFieldsLoadFile.Content = "";
            this.lblNumberOfFieldsList.Content = "";
            this.lblResult.Text = "";

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (File.Exists(files[0]))
                {
                    this.lblFilePath.Content = files[0].ToString();
                    this.lblNumberOfFieldsLoadFile.Content += Dgv.FieldNamesAsDisplayed.Count().ToString();
                    
                    List<string> result = ListOfFields.GetListOfFields(files[0]);

                    this.lblNumberOfFieldsList.Content += (result.Count == 1) ? result[0] : result.Count.ToString();

                    if(result.Count != Dgv.FieldNamesAsDisplayed.Count())
                    {
                        this.lblResult.Text = "Number of fields in the load file and field list do not match." 
                                               + "Sorting in this situation is not possible.";
                    }
                    else 
                    {
                        int NumMisMatch = 0;
                        foreach (string item in result)
                        {
                            if (!Dgv.FieldNamesAsDisplayed.Contains(item))
                            {
                                NumMisMatch++;
                                this.lblResult.Text = this.lblResult.Text + item + " ";
                            }
                            if (NumMisMatch > 0)
                            {
                                this.lblResult.Text = "These " + NumMisMatch + " fields in the Field List file do not exist in the load file.";                                
                            }
                        }

                        // If this condition is true, it means number of fields are matching in both load file and field list.
                        // If NumMisMatch is 0, it means all the fields exist in both field list and load file.
                        if (NumMisMatch == 0) 
                        {
                            //this.dgData.UpdateLayout();
                            Dgv.FieldNamesAsDisplayed.Clear();

                            for (int i = 0; i < result.Count; i++)
                            {
                                    Dgv.FieldNamesAsDisplayed.Add(result[i]);
                            } 
                            this.lblResult.Text = "Done! Please refresh.";
                        }
                    }                

                }
                else
                {
                    this.lblFilePath.Content = "File doesn't exist";
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
