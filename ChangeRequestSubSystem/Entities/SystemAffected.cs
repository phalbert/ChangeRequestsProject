using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("SystemsAffected")]
    public class SystemAffected: DbEntity<SystemAffected>
    {
        [PrimaryKey(PrimaryKeyType.Native, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string SystemName { get; set; }

        [Property(Length = 150)]
        public string SystemType { get; set; }

        [Property(Length = 300)]
        public string TypeOfChange { get; set; }

        [Property(Length = 6500)]
        public string Details { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string ModifiedBy { get; set; }

        [Property(Length = 50)]
        public string CreatedBy { get; set; }

        [Property(Length = 50)]
        public DateTime ModifiedOn { get; set; }

        [Property(Length = 50)]
        public DateTime CreatedOn { get; set; }
    }
}
