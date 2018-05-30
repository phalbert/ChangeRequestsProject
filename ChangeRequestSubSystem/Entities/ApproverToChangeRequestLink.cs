using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ApproverToChangeRequestLinks")]
    public class ApproverToChangeRequestLink : DbEntity<ApproverToChangeRequestLink>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string UserId { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string Decision { get; set; }

        [Property(Length = 50)]
        public string Reason { get; set; }

        [Property(Length = 50)]
        public string ModifiedBy { get; set; }

        [Property(Length = 50)]
        public string CreatedBy { get; set; }

        [Property(Length = 50)]
        public DateTime ModifiedOn { get; set; }

        [Property(Length = 50)]
        public DateTime CreatedOn { get; set; }

        public bool AlreadyExists()
        {
            return false;
        }


        public bool IsValidUpdate()
        {
            return true;
        }
    }
}
