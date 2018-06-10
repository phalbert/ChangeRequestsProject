using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("TimeBoundAccessRequests")]
    public class TimeBoundAccessRequest:DbEntity<TimeBoundAccessRequest>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string TBPAccessId { get; set; }

        [Property(Length = 50)]
        public string UserId { get; set; }

        [Property(Length = 50)]
        public string SystemCode { get; set; }

        [Property(Length = 50)]
        public string TypeOfAccess { get; set; }

        [Property(Length = 50)]
        public DateTime StartTime { get; set; }

        [Property(Length = 50)]
        public int DurationInMinutes { get; set; }

        [Property(Length = 50)]
        public string Reason { get; set; }

        [Property(Length = 50)]
        public string Status { get; set; }

        [Property(Length = 50)]
        public string Approver { get; set; }

        [Property(Length = 50)]
        public string ApproverReason { get; set; }
    }
}
