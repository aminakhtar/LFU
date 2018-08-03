using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
using Pdc.Plugins;

namespace LFU.Aqc
{
    /// <summary>
    /// Interaction logic for AqcUi.xaml
    /// </summary>
    public partial class AqcUi : Window
    {
        public AqcUi(LoadfileBase loadfile, string tablename)
        {
            InitializeComponent();

            this.Loadfile = loadfile;
            this.TableName = tablename;
        }

        private LoadfileBase Loadfile;
        private string TableName;

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            // build collection of delegates from the selected\queued plugins
            // loop through and 
            // invoke each delegate
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Plugins AvailablePlugins = new Plugins();

            DirectoryCatalog catalog = new DirectoryCatalog("Plugins");
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(AvailablePlugins);

            this.lstPluginsAvailable.ItemsSource = AvailablePlugins.AllPlugins;

        }

        private void lstPluginsAvailable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.tblStatus.Text = "";

            try
            {
                ListBox pluginList = (ListBox)e.Source;
                UIElement pluginSelection = (UIElement)pluginList.SelectedValue;
                UIElement pluginInstance = (UIElement)Activator.CreateInstance(pluginSelection.GetType());

                // inject the loadfile information
                ((IQcpPlugin)pluginInstance).SetLoadfileInfo(Loadfile);

                // inject connection to backing database
                ((IQcpPlugin)pluginInstance).SetConnection(LFU.Db.Connect.Connection);

                // inject table name for backing database table
                ((IQcpPlugin)pluginInstance).SetTableName(TableName);

                this.lstPluginsSelected.Items.Add(pluginInstance);

            }
            catch (Exception Ex)
            {
                this.tblStatus.Text = "Already added to queue? " + Ex.Message;
            }
        }

        private void lstPluginsSelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBox pluginList = (ListBox)e.Source;
                this.lstPluginsSelected.Items.Remove((UIElement)pluginList.SelectedValue);
            }
            catch (Exception Ex)
            {
                this.tblStatus.Text = "Error while attempting to remove plugin from queue. " + Ex.Message;
            }
        }




    }
}
