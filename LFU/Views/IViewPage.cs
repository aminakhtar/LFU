using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.Views
{
    public interface IViewPage
    {
        string TabName { get; set; }
        string TableName { get; set; }
        List<string> FieldNamesAsDisplayed { get; set; }
    }
}
