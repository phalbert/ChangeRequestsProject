using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("SystemTypes")]
    public class SystemType:DbEntity<SystemType>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property]
        public string TypeName { get; set; }

        [Property(Unique = true)]
        public string TypeCode { get; set; }
        
    }
}
