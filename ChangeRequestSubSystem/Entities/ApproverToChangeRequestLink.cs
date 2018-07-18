using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ApproversForChangeRequests")]
    public class ApproverForChangeRequest : DbEntity<ApproverForChangeRequest>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string ApproverId { get; set; }

        [Property(Length = 50)]
        public string Role { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string Decision { get; set; }

        [Property(Length = 6550)]
        public string Reason { get; set; }

        [Property]
        public bool? HasEmailBeenSent { get; set; }


        public bool AlreadyExists()
        {
            return false;
        }

        public override bool IsValid()
        {
            string propertiesThatCanBeNull = $"{nameof(Id)}|{nameof(HasEmailBeenSent)}";
            string nullCheckResult = SharedCommons.SharedCommons.CheckForNulls(this, propertiesThatCanBeNull);
            if (nullCheckResult != Globals.SUCCESS_STATUS_TEXT)
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = nullCheckResult;
                return false;
            }

            return base.IsValid();
        }


        public bool IsValidUpdate()
        {
            return true;
        }
    }
}
