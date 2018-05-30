using Castle.ActiveRecord;
using ChangeRequestSubSystem.ControlClasses;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class DbEntity<T> : ActiveRecordBase<T>
    {
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }

        public virtual bool IsValid()
        {
            return true;
        }


        public static T[] QueryWithStoredProc<T>(string storedProc, params object[] storedProcParameters) where T : new()
        {
            List<T> all = new List<T>();
            DataTable dt = DatabaseHandler.ExecuteStoredProc(storedProc, storedProcParameters);

            foreach (DataRow dr in dt.Rows)
            {
                T obj = new T();
                CopyParentArrayToChildProperty(dr, obj);
                all.Add(obj);
            }

            return all.ToArray();
        }

        public static void CopyParentArrayToChildProperty(DataRow parent, object child)
        {
            var childProperties = child.GetType().GetProperties();



            foreach (var childProperty in childProperties)
            {
                try
                {
                    object[] attrs = childProperty.GetCustomAttributes(false);
                    bool hasBeenSet = false;
                    
                    foreach (object attr in attrs)
                    {
                        PrimaryKeyAttribute pkAttribute = attr as PrimaryKeyAttribute;
                        if (pkAttribute != null)
                        {
                            string column = pkAttribute.Column;
                            if (column != null)
                            {
                                childProperty.SetValue(child, parent[column]);
                                hasBeenSet = true;
                                break;
                            }
                        }

                        PropertyAttribute propertyAttribute = attr as PropertyAttribute;
                        if (propertyAttribute != null)
                        {
                            string column = propertyAttribute.Column;
                            if (column != null)
                            {
                                childProperty.SetValue(child, parent[column]);
                                hasBeenSet = true;
                                break;
                            }
                        }
                    }
                    if (hasBeenSet) { continue; }
                    childProperty.SetValue(child, parent[childProperty.Name]);
                }
                catch (Exception)
                {

                }
            }
        }


        public virtual bool SetSuccessAsStatusInResponseFields()
        {
            StatusCode = Globals.SUCCESS_STATUS_CODE;
            StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            return true;
        }

        public virtual bool SetFailuresAsStatusInResponseFields(string Message)
        {
            StatusCode = Globals.FAILURE_STATUS_CODE;
            StatusDesc = Message;
            return true;
        }
    }
}
