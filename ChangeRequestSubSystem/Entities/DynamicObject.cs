using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;

namespace ChangeRequestSubSystem.Entities
{

    public static class DataTableExtensions
    {
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            if (table == null)
            {
                yield break;
            }

            foreach (DataRow row in table.Rows)
            {
                IDictionary<string, object> dRow = new ExpandoObject();

                foreach (DataColumn column in table.Columns)
                {
                    var value = row[column.ColumnName];
                    dRow[column.ColumnName] = Convert.IsDBNull(value) ? null : value;
                }

                yield return dRow;
            }
        }
    }

}
