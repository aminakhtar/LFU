using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.Db
{
    public abstract class PlatformConnect
    {
        public PlatformConnect() { }

        public string ConnectionStringTemplate { get; set; } // eg, data source = {0}; initial catalog = {1}; integrated security



    }
}
