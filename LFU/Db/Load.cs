using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Data;
using Pdc.Loadfiles;


namespace LFU.Db
{
    class Load : IDisposable
    {

        public Load()
        {
            // get the configured batch size from the app.config
            if (!int.TryParse(ConfigurationManager.AppSettings["SqlBatchSize"], out BatchSize))
            {
                BatchSize = 5000; // default
            }
        }

        public string TableName { get; set; }
        public string ErrorTableName { get; set; }
        public int BatchSize;
        public int CountTotalRecords = 0;      // RowNumber[0]
        public int CountTotalRecordsErrors = 0;
        public int CountLoadedRecords = 0;     // RowNumber[1]
        public int CountBadRecords = 0;        // RowNumber[2]
        public int CountErrors = 0;

        public void LoadToDatabase(LoadfileBase Loadfile)
        {
            Log.ErrorLog.AddMessage("Loading file: " + Loadfile.FileInformation.FullName);

            // builds the table and returns its name
            TableName = Connect.NewTable(
                Connect.GetTableName(Loadfile.FileInformation.FullName), 
                Loadfile.FieldNamesSelected
                );

            ErrorTableName = Db.Connect.NewTable(
                Connect.GetTableName(Loadfile.FileInformation.FullName) + "_error",
                new List<string>() { "Position", "Error", "Line"}
                );

            // ?VALUES? is only a placeholder for the list of input values we read from file
            string InsertStatement =
                "INSERT INTO [" +
                TableName +
                "] ([" +
                string.Join("],[", Loadfile.FieldNamesSelected) + // .Replace(" ", "_")
                "]) VALUES ('{0}');";

            // count of fields in a good row should match count of true values in Loadfile.FieldsSelected bool array
            // by default this should be same as count of FieldNames
            int RowLength = Loadfile.FieldsSelected.Count<bool>(x => x == true);

            // count of current number of uncommitted commands in the transaction
            int CurrentBatch = -1;

            SQLiteCommand MyCommand = new SQLiteCommand();
            SQLiteTransaction MyTransaction = Connect.Connection.BeginTransaction();
            string MyCommandString;

            // accumulate as many records as we declare with BatchSize and do not read past end of file
            while (CurrentBatch < BatchSize && !Loadfile.EndOfStream)
            {
                CurrentBatch = -1;
                try
                {
                    foreach (List<string> Record in Loadfile.NextRecord())
                    {
                        CountTotalRecords++;
                        CurrentBatch++;

                        for (int i = 0; i < Record.Count; i++)
                        {
                            Record[i] = Record[i].Replace("'", "''");
                        }

                        // build insert statement for one line
                        MyCommandString =
                            string.Format(
                                InsertStatement,
                                string.Join("','", Record)
                                );

                        MyCommand = new SQLiteCommand(MyCommandString, Connect.Connection, MyTransaction);
                        MyCommand.ExecuteNonQuery();
                        CountLoadedRecords++;
                    }
                }
                catch (RowException REx)
                {
                    CountBadRecords++;
                    AddError(
                        ErrorTableName,
                        REx.Position,
                        REx.ErrorMessage,
                        REx.Text
                        );
                }
                catch (Exception Ex)
                {
                    Log.ErrorLog.AddMessage(
                        "Error during load of "
                        + Loadfile.FileInformation.Name
                        + " at "
                        + CountTotalRecords.ToString("#,##0")
                        + Environment.NewLine
                        + Ex.Message
                        + Environment.NewLine
                        + Ex.StackTrace
                        );
                }
                
                MyTransaction.Commit();
                MyTransaction = Connect.Connection.BeginTransaction();
            }

            MyTransaction.Commit();
            FlushErrorBuffer();
            MyCommand = null;

            // count of records in errors table
            using (SQLiteCommand CommandCountErrors = new SQLiteCommand("SELECT COUNT(*) FROM [" + ErrorTableName + "];", Db.Connect.Connection))
            {
                CountTotalRecordsErrors = Convert.ToInt32(CommandCountErrors.ExecuteScalar());
            }

            Log.ErrorLog.AddMessage(
                "Loaded to " 
                + TableName 
                + " " 
                + CountLoadedRecords.ToString("#,##0") 
                + " / " 
                + CountTotalRecords.ToString("#,##0") 
                + " total records, " 
                + (CountBadRecords + CountErrors).ToString("#,##0") 
                + " error lines"
                );

        }
        

        #region "ERROR TABLE"

        private static Queue<string> ErrorBuffer = new Queue<string>();

        public static void FlushErrorBuffer()
        {
            StringBuilder allcommands = new StringBuilder();
            while (ErrorBuffer.Count > 0)
            {
                allcommands.Append(ErrorBuffer.Dequeue());
            }

            using (SQLiteCommand MyCommand = new SQLiteCommand(allcommands.ToString(), Connect.Connection))
            {
                int result = MyCommand.ExecuteNonQuery();
                Log.ErrorLog.AddMessage("Added " + result.ToString("#,##0") + " errors.");
            }
        }

        public static void AddError(string errortablename, int pos, string error, string line)
        {
            string commandstring = string.Format(
                "INSERT INTO [{0}] (Position, Error, Line) VALUES ({1}, '{2}', '{3}');",
                errortablename, pos, error, line
                );

            ErrorBuffer.Enqueue(commandstring);

            /*
            if (ErrorBuffer.Count > 1000)
            {
                FlushErrorBuffer();
            }*/

        }

        #endregion
        

        #region "IDISPOSABLE"

        private bool _Disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                }
                _Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
