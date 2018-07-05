using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRWebPortal
{
    public class SimpleDatabaseHandler<T> where T : new()
    {
        public static T[] QueryWithStoredProc(string storedProc, params object[] storedProcParameters)
        {
            List<T> all = new List<T>();
            DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet(storedProc, storedProcParameters).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {

                T obj = new T();
                CopyParentArrayToChildProperty(dr, obj);
                all.Add(obj);
            }

            return all.ToArray();
        }

        private static void CopyParentArrayToChildProperty(DataRow row, object child)
        {
            var childProperties = child.GetType().GetProperties();

            foreach (var childProperty in childProperties)
            {
                try
                {
                    string propName = childProperty.Name;
                    childProperty.SetValue(child, row[propName]);
                }
                catch (Exception)
                {
                    //property notfound in datatable
                }
            }
        }
    }
}