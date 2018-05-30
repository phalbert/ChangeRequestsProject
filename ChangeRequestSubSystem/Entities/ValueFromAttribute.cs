using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValueFromPropertyAttribute : Attribute
    {
        private string _propertyName = string.Empty;
        private System.Type _fromType = null;
        private System.Type _toType = null;

        public ValueFromPropertyAttribute(string propertyName)
        {
            this._propertyName = propertyName;
        }

        public ValueFromPropertyAttribute(string propertyName, System.Type
                  fromType)
        {
            this._propertyName = propertyName;
            this._fromType = fromType;
        }

        public ValueFromPropertyAttribute(string propertyName, System.Type fromType,
            System.Type toType)
        {
            this._propertyName = propertyName;
            this._fromType = fromType;
            this._toType = toType;
        }

        public string ValueFromProperty { get { return this._propertyName; } }

        public System.Type FromType { get { return this._fromType; } }
        public System.Type ToType { get { return this._toType; } }
    }
}


