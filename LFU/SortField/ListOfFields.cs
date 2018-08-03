using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFU.SortField
{
    public static class ListOfFields
    {
        public static List<string> GetListOfFields(string PathToTheFieldListFile)
        {
            List<string> result = new List<string>();
            string line;
            int counter = 0;
            try
            {
                using (StreamReader sr = new StreamReader(PathToTheFieldListFile))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        result.Add(line);
                        counter++;
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                result.Add("Could not read the file: " + ex.Message);                
            }
            return result;
        }
    }
}
