using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.Log
{
    public static class ErrorLog
    {
        static ErrorLog()
        {
            string alterrorlogpath = ConfigurationManager.AppSettings["ErrorLogPath"].ToString();

            if (!string.IsNullOrWhiteSpace(alterrorlogpath))
            {
                ErrorLogPath = Path.Combine(alterrorlogpath, "Lfu_ErrorLog.log");
            }
            else
            {
                ErrorLogPath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) , "Lfu_ErrorLog.log");
            }

            Sw = new StreamWriter(ErrorLogPath, true, Encoding.UTF8);
        }

        public static string ErrorLogPath;
        private static StreamWriter Sw;

        /// <summary>
        /// A timestamp of the current date and time in the form YYYYMMDD_HHMMSS_FFFF, where FFFF are fractions of a second.
        /// </summary>
        /// <returns>String timestamp</returns>
        public static string TimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff");
        }

        public static void Close()
        {
            if (Sw != null)
            {
                Sw.Close();
                Sw.Dispose();
            }
        }

        /// <summary>
        /// Write a message to the error log
        /// </summary>
        /// <param name="message">Any text message</param>
        public static void AddMessage(string message)
        {
            string timestamp = TimeStamp();
            int counter = 1;

            foreach (string m in message.Split('\n'))
            {
                Sw.WriteLine(timestamp + "\t" + counter.ToString() + "\t" + m.Replace("\r", ""));
                counter++;
            }
            
            Sw.Flush(); // write to disk as soon as we get a message
        }

        /// <summary>
        /// Write a message to the error log. Automatically add exception message and stack trace
        /// </summary>
        /// <param name="message">Any text message</param>
        /// <param name="ex">Any exception</param>
        public static void AddMessage(string message, Exception ex)
        {
            string timestamp = TimeStamp();
            int counter = 1;

            message += Environment.NewLine
                + ex.Message
                + Environment.NewLine
                + ex.StackTrace;

            foreach (string m in message.Split('\n'))
            {
                Sw.WriteLine(timestamp + "\t" + counter.ToString() + "\t" + m.Replace("\r", ""));
                counter++;
            }

            Sw.Flush(); // write to disk as soon as we get a message
        }

    }
}
