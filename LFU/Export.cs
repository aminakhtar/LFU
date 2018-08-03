using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdc.Loadfiles;

namespace LFU
{
    public static class Export
    {

        /// <summary>
        /// Write the loadfile data in the database to a file.
        /// Cannot sort descending.
        /// </summary>
        /// <param name="loadfile">Loadfile object</param>
        /// <param name="outputfilepath">Output path to new file, may overwrite existing file</param>
        /// <param name="tablename">The name of the table in our SQLite database</param>
        /// <param name="fieldnames">List of fields from the View, in their View order</param>
        /// <param name="sortfield">Optional, Name of the field on which to sort the output. 
        /// Default is _rowid_, ie, order in which rows were loaded</param>
        public static void Save(LoadfileBase loadfile, string outputfilepath, string tablename, List<string> fieldnames, string Command, string sortfield = "[rowid]")
        {
            string HeaderCommaSeparated = null;
            fieldnames.Remove("rowid");
            if (fieldnames.Count != 0)
            {
                HeaderCommaSeparated = "[" + string.Join("],[", fieldnames.ToArray()) + "]";
            }

            // initiate the loadfile object's streamwriter
            if (loadfile is ConcordanceDat || loadfile is Csv)
            {
                loadfile.Save(outputfilepath, ((IDelimited)loadfile).HasHeader, fieldnames);
            }
            else
            {
                loadfile.Save(outputfilepath);
            }

            // Select statement order by the sort field and the rows that we want to show it in the grid
            // in the future, we may want to provide output for only selected fields
            // *** DO NOT use brackets when building the following SQL statement.... brackets must be added when the sortState is set on the main form.
            //     We want to sort on >1 field in either asc or desc order, eg, "ORDER BY [Custodian-Name] DESC, [ControlNumber] ASC" ***
            string commandString;



            //If Command is null, it means save is pressed from a normal tab
            //Else Save is pressed from an Ad-Hoc Select tab:
            commandString = 
                Command == null 
                ?
                    "SELECT "
                    + HeaderCommaSeparated
                    + " FROM ["
                    + tablename
                    + "] ORDER BY "
                    + sortfield 
                :
                    Command;

            Log.ErrorLog.AddMessage("Exporting to " + outputfilepath + " using command: " + commandString);

            try
            {
                // build the command
                using (SQLiteCommand MySelectCommand = new SQLiteCommand(commandString, Db.Connect.Connection))
                {
                    using (SQLiteDataReader MyReader = MySelectCommand.ExecuteReader())
                    {
                        while (MyReader.Read())
                        {
                            List<string> nextRecord = new List<string>();

                            for (int i = 0; i < MyReader.FieldCount; i++)
                            {
                                nextRecord.Add(MyReader.GetString(i));
                            }

                            loadfile.WriteRecord(nextRecord);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                // log it and
                Log.ErrorLog.AddMessage("Error during Export operation to " + outputfilepath + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);

                // tell user
                global::System.Windows.MessageBox.Show(Ex.Message + Environment.NewLine + Ex.StackTrace);
            }
            finally
            {
                // this call to the loadfile object's close method ensures that the streamwriter flushes and closes
                loadfile.Close(); 
            }

        }

    }
}
