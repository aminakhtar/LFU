using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testParseOrderByClause
{

    public class SelectStatement
    {
        // SELECT fields FROM table WHERE filter ORDER BY sortstate
    }


    public class SortState
    {

        public SortState()
        { }

        public SortState(IEnumerable<string> fields)
        {
            Fields = fields.Select<string, Field>(x => new Field(x));
        }

        public class Field
        {

            public Field(string fieldname, bool isascending = true)
            {
                Name = fieldname;
                IsAscending = isascending;
            }

            public string Name;
            public bool IsAscending;
            public override string ToString()
            {
                return
                    "[" + Name + "]" + (IsAscending ? "" : " DESC");
            }
        }

        public IEnumerable<Field> Fields { get; set; }

        public string OrderByClause
        {
            get
            {
                return "ORDER BY " + string.Join(", ", Fields);
            }
        }

        public override string ToString()
        {
            return OrderByClause;
        }


    }
}
