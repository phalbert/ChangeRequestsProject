using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ApproversForChangeRequests")]
    public class ApproverToChangeRequestLink : DbEntity<ApproverToChangeRequestLink>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string UserId { get; set; }

        [Property(Length = 50)]
        public string Role { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string Decision { get; set; }

        [Property(Length = 50)]
        public string Reason { get; set; }

        [Property]
        public bool? HasEmailBeenSent { get; set; }


        public bool AlreadyExists()
        {
            return false;
        }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(ChangeRequestId))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = "Please Supply a ChangeRequestId";
                return false;
            }
            return true;
        }


        public bool IsValidUpdate()
        {
            return true;
        }
    }
}
