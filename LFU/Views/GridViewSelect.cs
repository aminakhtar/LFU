using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LFU.Views
{
    public class GridViewSelect
    {

        public GridViewSelect(string selectcommandstring, out int totalrowcount)
        {
            // get size of page from config
            int pagerowcount;
            if (!int.TryParse(ConfigurationManager.AppSettings["PageRowCount"], out pagerowcount))
            {
                this.PageRowCount = GridViewLoadfile.PageRowCountDefault; // hardcoded default if config fails
            }
            else
            {
                this.PageRowCount = pagerowcount; 
            }

            // store the select command that that user wrote
            SelectCommandString = selectcommandstring;

            // get a count of rows from the user's select command
            string CommandString =
                "SELECT count(*) FROM (" +
                SelectCommandString +
                ");";

            using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
            {
                using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                {
                    MyReader.Read();
                    TotalRowCount = MyReader.GetInt32(0);
                    totalrowcount = TotalRowCount; // the value in this variable is returned by out keyword
                }
            }

            LastPage = (TotalRowCount / this.PageRowCount) + 1;

        }

        #region "PROPERTIES"

        /// <summary>
        /// The select statement written by the user
        /// </summary>
        private string SelectCommandString { get; set; }

        /// <summary>
        /// Number of rows on one page
        /// </summary>
        public int PageRowCount;

        /// <summary>
        /// Total number of records in our current table, view, or selection
        /// </summary>
        public int TotalRowCount;

        /// <summary>
        /// The page we're currently viewing
        /// </summary>
        public int CurrentPage = 1;

        /// <summary>
        /// The last page available
        /// </summary>
        public int LastPage;

        #endregion

        public void Sort(DataGrid currentdatagrid, int startfromrow)
        {
            // int StartFromRow = (pagenumber - 1) * PageRowCount; 
            try
            {
                string CommandString =
                    "SELECT * FROM ("
                    + SelectCommandString    // remove all semicolons just in case....
                    + ")"
                    + " LIMIT "
                    + PageRowCount
                    + " OFFSET "
                    + startfromrow
                    + ";";

                // build the command
                using (SQLiteCommand MySelectCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    SQLiteDataAdapter MySqliteDataAdapter = new SQLiteDataAdapter(MySelectCommand);

                    //Building the data table
                    DataTable Dt = new DataTable(); // no name for the ad hoc select tables right now

                    //Filling the data table
                    MySqliteDataAdapter.Fill(Dt);

                    currentdatagrid.ItemsSource = Dt.DefaultView;
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage(
                    "Failed on Sort operation" + 
                    Environment.NewLine + 
                    Ex.Message + 
                    Environment.NewLine + 
                    Ex.StackTrace
                    );

                global::System.Windows.MessageBox.Show(Ex.Message);
            }

        }



    }
}
