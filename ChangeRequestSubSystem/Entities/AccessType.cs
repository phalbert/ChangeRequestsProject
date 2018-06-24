using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("AccessTypes")]
    public class AccessType:DbEntity<AccessType>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string SystemTypeCode { get; set; }

        [Property(Length = 50, Unique = true)]
        public string AccessTypeCode { get; set; }

        [Property(Length = 50)]
        public string AccessTypeName { get; set; }
    }
}
