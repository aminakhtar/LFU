using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdc.Plugins;

namespace LFU.Aqc
{
    class Plugins
    {
        public Plugins()
        {
            AllPlugins = new List<IQcpPlugin>();
        }

        [ImportMany(typeof(IQcpPlugin))]
        public IEnumerable<IQcpPlugin> AllPlugins { get; set; }
    }
}
