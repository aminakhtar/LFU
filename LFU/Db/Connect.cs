using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LFU.Db
{
    public static class Connect
    {
        static Connect()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["SQLiteDatabase"].ConnectionString;

            // expressed in megabytes
            InMemoryThreshhold = (1024 * 1024) * Convert.ToInt32(ConfigurationManager.AppSettings["InMemoryThreshhold"]);

            // alternative database storage path
            AlternativeDatabaseStoragePath = ConfigurationManager.AppSettings["AlternativeDatabaseStoragePath"];

            PlatformConnections = new Dictionary<string, SqlConnection>();

        }

        #region "PROPERTIES"

        /// <summary>
        /// disabled for the moment... System will crash if user has an in-memory database and THEN opens a bigger-than-RAM database.
        /// </summary>
        private static long InMemoryThreshhold;

        /// <summary>
        /// By default, TEMP environment settings are used. This can be overridden in the config file.
        /// </summary>
        private static string AlternativeDatabaseStoragePath;

        /// <summary>
        /// Fullpath to the backing database file
        /// </summary>
        public static string DatabasePath { get; private set; }

        /// <summary>
        /// The connection string used to connect to the backing database
        /// </summary>
        public static string ConnectionString { get; private set; }

        /// <summary>
        /// Connection to the backing database
        /// </summary>
        public static SQLiteConnection Connection { get; private set; }

        /// <summary>
        /// The current connection state to the backing database
        /// </summary>
        public static System.Data.ConnectionState State
        {
            get
            {
                return Connection.State;
            }

            private set { }
        }

        /// <summary>
        /// Connections to the processing platforms
        /// key is name of database, value is a SqlConnection object
        /// </summary>
        public static Dictionary<string, SqlConnection> PlatformConnections { get; set; }


        #endregion

        /// <summary>
        /// Create a database and return the path to its file
        /// </summary>
        /// <returns>Fullpath to the database file</returns>
        public static string Initialize(long loadfilesize)
        {
            // Create a database
            string dbstoragepath = (
                !Directory.Exists(AlternativeDatabaseStoragePath)
                ? Path.GetTempPath()
                : AlternativeDatabaseStoragePath
                );

            DatabasePath = Path.Combine(
                dbstoragepath,
                "LFU_" + LFU.Log.ErrorLog.TimeStamp() + ".s3db"
                );

            /*
            if (loadfilesize < InMemoryThreshhold)
            {
                DatabasePath = ":memory:";
            }
            else
            {
            }
            */

            try
            {
                if (DatabasePath != ":memory:")
                {
                    if (File.Exists(DatabasePath))
                    {
                        File.Delete(DatabasePath);
                    }
                    SQLiteConnection.CreateFile(DatabasePath);
                    Log.ErrorLog.AddMessage("Created database: " + DatabasePath);
                }
                else
                {
                    Log.ErrorLog.AddMessage("Creating database in memory.... ");
                    ConnectionString += "; New=true";
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed to create SQLite database. " + Ex.Message + Environment.NewLine + Ex.StackTrace);
            }

            // Connect to it

            try
            {
                Connection = new SQLiteConnection(
                    string.Format(ConnectionString, DatabasePath)
                    );
                Connection.Open();
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed to connect to database: " + DatabasePath + Environment.NewLine + "Connection string: " + ConnectionString + Environment.NewLine + Ex.Message);
            }

            return DatabasePath;

        }


        #region "GENERAL TABLE HANDLING"

        /// <summary>
        /// Transform a filepath into a table name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetTableName(string filename)
        {
            string tablename = Path.GetFileName(filename).Replace(" ", "_").Replace(".", "_");
            return tablename;
        }

        /// <summary>
        /// Count of records in a table
        /// </summary>
        /// <param name="tablename">Name of the table to get count of records from</param>
        /// <returns>Number of records in the table</returns>
        public static int RecordCount(string tablename)
        {
            int Result = 0;

            string commandstring = "SELECT COUNT(rowid) FROM [" + tablename + "]; ";

            using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Connection))
            {
                Result = int.Parse(MyCommand.ExecuteScalar().ToString());
            }

            return Result;
        }


        /// <summary>
        /// Create a new table in the SQLite database.
        /// Drops the table if it exists and then creates.
        /// The loadfile's filename will be used, spaces and periods will be replaced with underscores.
        /// </summary>
        /// <param name="fullpath">Fullpath to the loadfile</param>
        /// <param name="fieldnames">Field names, nominal loadfile headers</param>
        /// <returns>Name of table created</returns>
        public static string NewTable(string tablename, List<string> fieldnames)
        {
            string CommandString = "";

            try
            {
                // we're not checking if the table exists... if it does, then drop it.
                CommandString =
                    "DROP TABLE IF EXISTS [" +
                    tablename +
                    "]; CREATE TABLE [" +
                    tablename +
                    "] ([" +
                    string.Join("] TEXT, [", fieldnames) + 
                    "] TEXT);";

                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Connection))
                {
                    MyCommand.ExecuteNonQuery();
                    Log.ErrorLog.AddMessage("Added new table: " + tablename + " using command: " + CommandString);
                }

            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed to create new table with command: " + CommandString + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
                global::System.Windows.MessageBox.Show("Failed to create new table. " + Ex.Message);
            }

            return tablename;
        }


        /// <summary>
        /// Rename a SQLite table, eg, after the file has been saved to a new filename
        /// </summary>
        /// <param name="oldtablename">Current table name</param>
        /// <param name="newtablename">Desired table name</param>
        /// <returns>Count of rows affected, 1 if success, 0 if fail</returns>
        public static int RenameTable(string oldtablename, string newtablename)
        {
            int result = 0;
            string CommandString = "ALTER TABLE [" + oldtablename + "] RENAME TO " + newtablename + ";";

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Connection))
                {
                    result = MyCommand.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while renaming table " + oldtablename + " to " + newtablename + " using command: " + CommandString + Environment.NewLine + Ex.Message);
            }

            return result;

        }

        /// <summary>
        /// Drop (delete) a table from the database
        /// </summary>
        /// <param name="tablename">Name of the table to drop</param>
        public static void DropTable(string tablename)
        {
            string commandstring = "";
            try
            {
                commandstring = "DROP TABLE [" + tablename + "];";
                using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Connection))
                {
                    MyCommand.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error dropping table " + tablename + " using command: " + commandstring + Environment.NewLine + Ex.Message);
            }
        }


        public static List<string> UpdateDateTime(string tablename, string sourcecolumn, string destcolumn, string sourceFormat, string destFormat)
        {
            List<string> ErrorList = new List<string>();
            List<string> temp = new List<string>();
            List<int> tempRowids = new List<int>();
            List<string> result = new List<string>();

            string commandstring = "SELECT rowid, ["+ sourcecolumn + "] FROM ["+ tablename + "]; ";

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Connection))
                {
                    using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                    {
                        while (MyReader.Read())
                        {
                            tempRowids.Add(MyReader.GetInt32(0));
                            temp.Add(MyReader.GetString(1));
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error Select query for update  " + commandstring + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
            }

            foreach (var item in temp)
            {
                if (item != "")
                {
                    try
                    {

                        #region
                        CultureInfo enUS = new CultureInfo("en-US");
                        DateTime OutPutDate;

                        if (DateTime.TryParseExact(item, sourceFormat, enUS, DateTimeStyles.AllowWhiteSpaces, out OutPutDate))
                        {
                            switch (destFormat)
                            {
                                case "MM/dd/yyyy":
                                    result.Add(OutPutDate.ToString("d", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "h:mm tt":
                                    result.Add(OutPutDate.ToString("t"));
                                    break;

                                case "M/d/yyyy h:mm tt":
                                    result.Add(OutPutDate.ToString("g"));
                                    break;

                                case "hh:mm:ss":
                                    result.Add(OutPutDate.ToString("T", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "MM-dd-yyyy":
                                    result.Add(OutPutDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "MM-dd-yy":
                                    result.Add(OutPutDate.ToString("MM-dd-yy", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "yyyyddmm":
                                    result.Add(OutPutDate.ToString("yyyyddMM", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "MM/dd/yyyy hh:mm":
                                    result.Add(OutPutDate.ToString("MM/dd/yyyy hh:mm", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "dd/MM/yyyy HH:mm":
                                    result.Add(OutPutDate.ToString("dd/MM/yyyy HH:mm", DateTimeFormatInfo.InvariantInfo));
                                    break;

                                case "MM/dd/yyyy hh:mm tt":
                                    result.Add(OutPutDate.ToString("MM/dd/yyyy hh:mm tt", DateTimeFormatInfo.InvariantInfo));
                                    break;
                            }
                        }
                        else
                        {
                            ErrorList.Add(item);
                        }

                        #endregion
                    }
                    catch (Exception Ex)
                    {
                        Log.ErrorLog.AddMessage("Process date and time error: " + Ex.Message);
                    }
                }
                else
                {
                    result.Add("");
                }
            }

            //result.Count should be the same as temp.Count

            int index = 0;
            foreach (var resultVal in result)
            {
                string CommandString =
                "Update [" + tablename + "] Set["+ destcolumn +"] = '"+ resultVal.TrimEnd(' ') + "'" +
                " Where rowid = "+ tempRowids[index] + ";";
                index++;

                try
                {
                    using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                    {
                        MyCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception Ex)
                {
                    Log.ErrorLog.AddMessage("Error while inserting the date and time into the table after update: " + CommandString + Environment.NewLine +
                        Ex.Message + Environment.NewLine +
                        Ex.StackTrace
                        );
                }
            }

            return ErrorList;
        }


        public static void ClearColumns(string tablename, string columnname)
        {
            string CommandString =
            "Update [" +
            tablename +
            "]  Set [" +
            columnname +
            "] = ''" +
            "; ";

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    MyCommand.ExecuteNonQuery();
                }
            }catch(Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while cleaning a field the table: " + CommandString + Environment.NewLine +
                Ex.Message + Environment.NewLine +
                Ex.StackTrace);
            }
        }


        public static void MergeColumns(string tablename, string FirstCol,string StringBetween, string SecondCol, string ResultCol)
        {
            if (StringBetween == "") StringBetween = "''";

            string CommandString = "UPDATE [" +
                                    tablename + 
                                    "] Set [" + 
                                    ResultCol +
                                    "] = " +
                                    "[" + FirstCol + "] || " + StringBetween + " || [" + SecondCol + "]";
            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    MyCommand.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while merging two fields: " + CommandString + Environment.NewLine +
                Ex.Message + Environment.NewLine +
                Ex.StackTrace);
            }
        }

        /// <summary>
        /// Drop (delete) a column from a table
        /// </summary>
        /// <param name="columnname">Name of the column to drop</param>
        public static void DropColumn(string tablename, string columnname)
        {
            // let's use a temporary name for our table that is both
            // relevant and guaranteed unique
            string tempname = tablename + "_" + Log.ErrorLog.TimeStamp();

            // collect the column names from the table before we change its name
            List<string> AllColumns = Fields(tablename);
            AllColumns.Remove(columnname);

            // rename old table with temp name
            RenameTable(tablename, tempname);
            
            // create new table with original name
            NewTable(tablename, AllColumns);

            // move records into new table
            string CommandString =
                "INSERT INTO [" +
                tablename +
                "] ([" +
                string.Join("], [", AllColumns) +
                "]) SELECT [" +
                string.Join("], [", AllColumns) +
                "] FROM [" +
                tempname +
                "]; ";

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Db.Connect.Connection))
                {
                    MyCommand.ExecuteNonQuery();
                    Log.ErrorLog.AddMessage("Dropped a column from table: [" + tablename + "] using command: " + CommandString);
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while dropping a column from table: [" + tablename + "] using command: " + CommandString + Environment.NewLine +
                    Ex.Message + Environment.NewLine + 
                    Ex.StackTrace
                    );
            }

            // drop old table
            DropTable(tempname);

        }

        public static void AddColumn(string tablename, string newcolumnname)
        {
            string CommandString =
                "ALTER TABLE [" +
                tablename +
                "] ADD COLUMN [" +
                newcolumnname +
                "] TEXT DEFAULT '';"; // nulls make the saver\exporter class very angry

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(CommandString, Connection))
                {
                    MyCommand.ExecuteNonQuery();
                    Log.ErrorLog.AddMessage("Added column: " + newcolumnname + " to table: " + tablename);
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error adding column: " + newcolumnname + " to table: " + tablename + Environment.NewLine +
                    Ex.Message + Environment.NewLine +
                    Ex.StackTrace
                    );
            }
        }

        public static void AddColumns(string tablename, IEnumerable<string> newcolumnnames)
        {
            foreach (string Ncn in newcolumnnames)
            {
                AddColumn(tablename, Ncn);
            }
        }


        /// <summary>
        /// Gets the database and table name and the column that we want to build the index on it
        /// This method doesn't have any output and just it makes the index on a column
        /// </summary>
        /// <param name="tablename">The table on which to create an index</param>
        /// <param name="indexcolumn">the column from tablename on which to create an index</param>
        public static void ApplyIndex(string tablename, string indexcolumn)
        {
            try
            {
                string IndexSqlite =
                    "CREATE INDEX IF NOT EXISTS ["
                    + indexcolumn
                    + "] ON ["
                    + tablename
                    + "] (["
                    + indexcolumn
                    + "]);";

                using (SQLiteCommand MySelectCommand = new SQLiteCommand(IndexSqlite, Db.Connect.Connection))
                {
                    MySelectCommand.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Failed on Index operation on column " + indexcolumn + Environment.NewLine + Ex.Message);
                global::System.Windows.MessageBox.Show(Ex.Message);
            }
        }

        #endregion


        /// <summary>
        /// List of names of all tables in the database
        /// </summary>
        /// <returns>List of string representing table names</returns>
        public static List<string> Tables()
        {
            List<string> temp = new List<string>();
            string commandstring = "SELECT [name] FROM [sqlite_master] WHERE [type] = 'table'; ";

            try
            {               
                using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Connection))
                {
                    using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                    {
                        while (MyReader.Read())
                        {
                            temp.Add(MyReader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error querying database for table names using command " + commandstring + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
            }

            return temp;
        }

        /// <summary>
        /// List of names of all the fields in the user-made tables of the database
        /// </summary>
        /// <returns>List of string representing all field names in all tables</returns>
        public static List<string> Fields()
        {
            List<string> tempFields = new List<string>();

            try
            {
                foreach (string table in Tables())
                {
                    tempFields.AddRange(Fields(table));
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while querying database for all field names." + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
            }

            return tempFields;
        }

        /// <summary>
        /// List of names of the fields from a given table
        /// </summary>
        /// <param name="tablename">Name of a user-made table in the database</param>
        /// <returns>List of string representing all field names in a given table</returns>
        public static List<string> Fields(string tablename)
        {
            List<string> tempFields = new List<string>();
            string commandstring = "SELECT [sql] FROM [sqlite_master] WHERE [tbl_name] = '" + tablename.Trim() + "' AND [type] = 'table'; ";

            try
            {
                using (SQLiteCommand MyCommand = new SQLiteCommand(commandstring, Connection))
                {
                    using (SQLiteDataReader MyReader = MyCommand.ExecuteReader())
                    {
                        while (MyReader.Read())
                        {
                            // tempFields.Add(MyReader.GetString(0));
                            foreach (Match M in Regex.Matches(MyReader.GetString(0), @"\[(?<field>(.+?))\]", RegexOptions.Compiled | RegexOptions.IgnoreCase)) 
                            {
                                tempFields.Add(M.Groups["field"].Value);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.ErrorLog.AddMessage("Error while querying database for field names for table " + tablename + " using command " + commandstring + Environment.NewLine + Ex.Message + Environment.NewLine + Ex.StackTrace);
            }

            // the first item in the results is the name of the table
            // we don't want it
            tempFields.RemoveAt(0); // why is the name of the table in the results of this query?

            return tempFields;
        }




    }
}

