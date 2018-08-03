using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.Log
{
    /// <summary>
    /// Very simple timer. 
    /// Start to set the time to Now. ElapsedTime to return TimeSpan
    /// </summary>
    public static class Timer
    {
        private static DateTime StartTime;

        public static void Start()
        {
            StartTime = DateTime.Now;
        }

        public static TimeSpan ElapsedTime()
        {
            return DateTime.Now - StartTime;
        }
    }

}
