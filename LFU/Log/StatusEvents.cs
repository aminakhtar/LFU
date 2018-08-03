using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.Log
{

    public delegate void StatusEventHandler(object sender, StatusEventArgs e);

    public class StatusEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public StatusEventArgs(string message)
            : base()
        {
            this.Message = message;
        }
    }

}
