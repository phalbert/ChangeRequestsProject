using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public static class DynamicExtensions
    {
        //public static T FromDynamic<T>(this IDictionary<string, object> dictionary)
        //{
        //    var bindings = new List<MemberBinding>();
        //    foreach (var sourceProperty in typeof(T).GetProperties().Where(x => x.CanWrite))
        //    {
        //        var key = dictionary.Keys.SingleOrDefault(x => x.Equals(sourceProperty.Name, StringComparison.OrdinalIgnoreCase));
        //        if (string.IsNullOrEmpty(key)) continue;
        //        var propertyValue = dictionary[key];
        //        bindings.Add(Expression.Bind(sourceProperty, Expression.Constant(propertyValue)));
        //    }
        //    Expression memberInit = Expression.MemberInit(Expression.New(typeof(T)), bindings);
        //    return Expression.Lambda<Func<T>>(memberInit).Compile().Invoke();
        //}

        public static T FromDataTableToObject<T>(this DataRow sourceObject) where T : new()
        {
            var result_obj = new T();
            CopyParentArrayToChildProperty(sourceObject, result_obj);
            return result_obj;
        }

        public static void Copy(object parent, object child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }

        public static void CopyParentArrayToChildProperty(DataRow parent, object child)
        {
            var childProperties = child.GetType().GetProperties();


            foreach (var childProperty in childProperties)
            {
                try
                {
                    childProperty.SetValue(child, parent[childProperty.Name]);
                }
                catch (Exception)
                {

                }
            }
        }

    }


}
