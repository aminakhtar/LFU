using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testParseOrderByClause
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Test Parse OrderBy Clause\n");

            List<string> fields = new List<string>() { "Custodian", "Docid2", "attachments" };

            SortState ss = new SortState(fields);

            Console.WriteLine(ss);



            Console.Write("\nPress ENTER to continue....");
            Console.ReadLine();

        }
    }
}
