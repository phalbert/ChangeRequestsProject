using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public static class Converter
    {
        public static object ConvertAll(object[] fromObjects, Type toType)
        {
            if (fromObjects != null)
            {
                ArrayList list = new ArrayList(fromObjects.Length);

                foreach (object obj in fromObjects)
                {
                    list.Add(Convert(obj, toType));
                }

                return list.ToArray(toType);

            }
            return null;
        }

        public static object Convert(object fromObject, Type toType)
        {
            if (fromObject == null) return null;
            object returnObject = Activator.CreateInstance(toType);

            PropertyInfo[] infos = returnObject.GetType().GetProperties();
            foreach (PropertyInfo property in infos)
            {
                ValueFromPropertyAttribute[] attributes = (ValueFromPropertyAttribute [])property.GetCustomAttributes(typeof (ValueFromPropertyAttribute), false);

                if (attributes.Length > 0)
                {
                    PropertyInfo fromProperty = fromObject.GetType().GetProperty(attributes[0].ValueFromProperty);

                    if (attributes[0].FromType == null)
                    {

                        property.SetValue(returnObject, fromProperty.GetValue(fromObject, null), null);
                    }
                    else
                    {
                        if (fromProperty.PropertyType.IsArray)
                        {
                            property.SetValue(returnObject, ConvertAll((object[]) fromProperty.GetValue(fromObject, null), attributes[0].ToType), null);
                        }
                        else
                        {
                            property.SetValue(returnObject, Convert(fromProperty.GetValue(fromObject, null), property.PropertyType), null);
                        }
                    }
                }
            }

            return returnObject;
        }

    }
}
