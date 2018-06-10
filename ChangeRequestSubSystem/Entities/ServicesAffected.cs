using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ServicesAffected")]
    public class ServicesAffected:DbEntity<ServicesAffected>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string ServiceName { get; set; }

        [Property(Length = 50)]
        public string ServiceCode { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }
        
    }
}
