using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.CodeCompletion;

namespace LFU
{
    /*
    public delegate void SqlConsoleRefreshEventHandler(object sender, SqlConsoleRefreshEventArgs e);

    public class SqlConsoleRefreshEventArgs : EventArgs
    {
        public SqlConsoleRefreshEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
    */


    /// <summary>
    /// Interaction logic for SqlConsoleWindow.xaml
    /// </summary>
    public partial class SqlConsoleWindow : Window
    {
        /// <summary>
        /// When a SQL command is executed
        /// </summary>
        public event Log.StatusEventHandler SqlConsoleRefresher;

        private static List<string> InputHistory = new List<string>();
        private static int HistoryIndex = 0;
        private SQLiteTransaction LastTransaction = null;
        private bool keyFirstChar = true;
        string[] AllKeywords;
        private TabControl TabControlMain; // reference to the main TabControl on the main window

        #region "CONSTRUCTOR(S)"

        public SqlConsoleWindow(TabControl tabcontrolmain)
        {
            InitializeComponent();

            // need a reference to this from the main window
            TabControlMain = tabcontrolmain;

            // SQL console history persists after you close the window since it is static
            if (InputHistory.Count == 0)
            {
                // first entry in history buffer is empty string
                InputHistory.Add("");
            }

            // assemble a list of keywords for autocomplete
            // We rebuild this list of keywords each time so that it will include new tables and fields.

            // SQLite keywords are kept in a file distributed with this app
            List<string> tempKeywords = new List<string>(
                File.ReadAllLines(@"Db\SqliteKeywords.txt")
                );

            // All table names and all field names from every table are also useful for autocomplete
            tempKeywords.AddRange(Db.Connect.Tables());
            tempKeywords.AddRange(Db.Connect.Fields());

            // all keywords in one collection
            AllKeywords = tempKeywords.ToArray<string>();

        }

        #endregion    



        #region "METHODS"

        public void PostResult(string result)
        {
            // write the response to the console history
            TextBlock tb = new TextBlock();
            tb = new TextBlock();
            tb.Foreground = Brushes.DarkRed;
            tb.Margin = new Thickness(0, 0, 0, 5); // keep some space between command-result pairs
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Text = result;
            // put the LAST result right after the BEGINNING of the collection 
            // so the result FOLLOWS the command
            this.stackConsoleHistory.Children.Insert(1, tb); 
        }


        public void BuildNewSelectView(string selectcommandstring, out int rowcount)
        {
            TabItem NewTab = new TabItem();

            TextBlock NewTabTitle = new TextBlock();
            // NewTabTitle.Text = Db.Connect.GetTableName(selectedloadfile.FileInformation.FullName); // let's use the name of the file instead
            NewTabTitle.Text = "Ad Hoc Select";

            Button NewTabCloseButton = new Button();
            NewTabCloseButton.Content = (char)215;
            NewTabCloseButton.FontSize = 10;
            NewTabCloseButton.Height = 15;
            NewTabCloseButton.Width = 15;
            NewTabCloseButton.Click += tabHeaderCloseButton_OnClick;
            NewTabCloseButton.Margin = new Thickness(5, 0, 0, 0);
            NewTabCloseButton.Padding = new Thickness(0);
            NewTabCloseButton.BorderThickness = new Thickness(0);
            NewTabCloseButton.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            NewTabCloseButton.ToolTip = "Close";

            StackPanel NewTabHeader = new StackPanel();
            NewTabHeader.Orientation = Orientation.Horizontal;
            NewTabHeader.Children.Add(NewTabTitle);
            NewTabHeader.Children.Add(NewTabCloseButton);

            NewTab.Header = NewTabHeader;

            Views.LoadfilePage LoadFilePageSelect = new Views.LoadfilePage();
            LoadFilePageSelect.LoadAdHocSelect(selectcommandstring, out rowcount);
            NewTab.Content = LoadFilePageSelect.Content;
            TabControlMain.Items.Add(NewTab);
            TabControlMain.SelectedItem = NewTab;
        }


        public void tabHeaderCloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            UIElement item = (UIElement)sender; // sender is the button
            var closeme = LogicalTreeHelper.GetParent(item);
            this.TabControlMain.Items.Remove(LogicalTreeHelper.GetParent(closeme));

        }


        private void ExecuteCommand()
        {
            // remove all whitespace
            // sqlconsole doesn't like tab characters
            string command = Regex.Replace(this.tbConsoleInput.Text.Trim(), @"\s+", " ").Replace(";", "");            

            InputHistory.Add(command);
            HistoryIndex++;

            TextBlock tb = new TextBlock();
            tb.Foreground = Brushes.DarkBlue;
            tb.Text = command;
            // put the LAST command at the BEGINNING of the collection
            this.stackConsoleHistory.Children.Insert(0, tb); 
            this.tbConsoleInput.Clear();

            Log.Timer.Start();
            
            using (SQLiteCommand MyCommand = new SQLiteCommand(command, Db.Connect.Connection, LastTransaction))
            {
                string[] c = command.Split(
                    new char[1] { ' ' }, 
                    2, 
                    StringSplitOptions.RemoveEmptyEntries
                    );

                int result = 0;
                string message = "";

                try
                {
                    switch (c[0].ToUpper())
                    {
                        case "SELECT":
                            BuildNewSelectView(MyCommand.CommandText, out result);
                            message = result.ToString("#,##0") + " rows selected";
                            break;

                        case "UPDATE":
                            result = MyCommand.ExecuteNonQuery();
                            message = result.ToString("#,##0") + " rows updated";
                            break;

                        case "INSERT":
                            result = MyCommand.ExecuteNonQuery();
                            message = result.ToString("#,##0") + " rows inserted";
                            break;

                        case "DELETE":
                            result = MyCommand.ExecuteNonQuery();
                            message = result.ToString("#,##0") + " rows deleted";
                            break;

                        case "DROP":
                            result = 666;
                            message = "DROP TABLE is disabled. The database will be deleted when LFU is normally closed.";
                            break;

                        case "TABLES":
                            List<string> tables = Db.Connect.Tables();
                            result = tables.Count;
                            message = string.Join(Environment.NewLine, tables);
                            break;

                        case "ALTER":
                            result = MyCommand.ExecuteNonQuery();
                            message = "Alteration complete. (" + result.ToString() + ")";
                            break;

                        // Let the users play in traffic -ds 2015-09-23
                        default:
                            result = MyCommand.ExecuteNonQuery();
                            message = result.ToString("#,##0") + " records affected.";
                            break;                            
                    }

                    // show in console history
                    PostResult(message);

                    // refresh the MainWindow
                    if (SqlConsoleRefresher != null && result > 0)
                    {
                        SqlConsoleRefresher(
                            this, 
                            new Log.StatusEventArgs(message + " (" + Log.Timer.ElapsedTime() + ")")
                            );
                    }

                }
                catch (Exception Ex)
                {
                    PostResult(Ex.Message);
                }
            }
        }


        private void IndexUp()
        {
            if (HistoryIndex < InputHistory.Count-1)
            {
                HistoryIndex++;
            }
        }


        private void IndexDown()
        {
            if (HistoryIndex > 0)
            {
                HistoryIndex--;
            }
        }


        #endregion

        
        #region "INTERFACE EVENTS"

        private void tbConsoleInput_KeyUp(object sender, KeyEventArgs e)
        {
            // disable input in case the user twitches
            this.tbConsoleInput.IsEnabled = false;

            // browse the command history
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                switch (e.Key)
                {
                    case Key.Up:
                        IndexUp();
                        this.tbConsoleInput.Text = InputHistory[HistoryIndex];
                        break;

                    case Key.Down:
                        IndexDown();
                        this.tbConsoleInput.Text = InputHistory[HistoryIndex];
                        break;
                }
                
            }
            // execute the command
            else if (e.Key == Key.F5)
            {
                ExecuteCommand();
            }

            this.tbConsoleInput.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LastTransaction != null)
            {
                try
                {
                    LastTransaction.Commit();
                }
                catch (Exception Ex)
                {
                    Log.ErrorLog.AddMessage("Error closing the SQL Console window: " + Ex.Message);
                }
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // let users open only one 
            if (!SqlConsoleHelpWindow.IsHelpShown)
            {
                SqlConsoleHelpWindow Schw = new SqlConsoleHelpWindow();
                Schw.Show();
            }
        }

        #endregion


        #region "AUTOCOMPLETE"

        private void tbConsoleInput_KeyDown(object sender, KeyEventArgs e)
        {
            bool KeySuggest = false;

            if (e.Key == Key.Space)
            {
                keyFirstChar = true;
            }

            if (keyFirstChar && e.Key != Key.Space) // If it is the first character of the box OR (first character of the word)
            {
                keyFirstChar = false;

                for (int i = 0; i < AllKeywords.Length; i++)
                {
                    // AllKeywords[i].ToUpper().StartsWith(e.Key.ToString().ToUpper())
                    if (string.Equals(AllKeywords[i][0].ToString(), e.Key.ToString(), StringComparison.CurrentCultureIgnoreCase)) // If in the resulat at least one command start with that ch
                    {
                        KeySuggest = true;
                    }
                }
            }

            if (KeySuggest)
            {
                CompletionWindow completionWindow = new CompletionWindow(this.tbConsoleInput.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

                for (int i = 0; i < AllKeywords.Length; i++)
                {
                    if (string.Equals(AllKeywords[i][0].ToString(), e.Key.ToString(), StringComparison.CurrentCultureIgnoreCase))  //  AllKeywords[i].StartsWith(e.Key.ToString())
                        data.Add(
                            new MyCompletionData(AllKeywords[i])
                            );
                }

                if (data.Count > 0)
                {
                    completionWindow.Show();
                    completionWindow.Closed += delegate { completionWindow.Close(); completionWindow = null; };
                }

            }
        }

        public class MyCompletionData : ICompletionData
        {
            public MyCompletionData(string text)
            {
                this.Text = text;
            }

            public ImageSource Image
            {
                get { return null; }
            }

            public string Text { get; private set; }

            // Use this property if you want to show a fancy UIElement in the list.
            public object Content
            {
                get { return this.Text; }
            }

            public object Description
            {
                get { return null; } // get { return "Description for " + this.Text; }
            }

            public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
            {
                int StartPoint = completionSegment.Offset;
                int length = completionSegment.Length;
                textArea.Document.Replace(StartPoint, length, this.Text);
            }

            public double Priority { get; set; }

        }

        #endregion



    }
}

