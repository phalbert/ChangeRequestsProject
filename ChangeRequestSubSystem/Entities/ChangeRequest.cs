using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ChangeRequests")]
    public class ChangeRequest:DbEntity<ChangeRequest>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Unique = true)]
        public string ChangeRequestId { get; set; }

        [Property (Length =500)]
        public string Title { get; set; }

        [Property(Length = 50)]
        public string RequesterName { get; set; }

        [Property(Length = 50)]
        public string RequesterCompany { get; set; }

        [Property(Length = 50)]
        public string RequesterEmail { get; set; }

        [Property(Length = 50)]
        public string RequesterPhone { get; set; }

        [Property(Length = 50)]
        public string ImplementerName { get; set; }

        [Property(Length = 50)]
        public string ImplementerEmail { get; set; }

        [Property(Length = 50)]
        public string ImplementerPhone { get; set; }

        [Property(Length = 50)]
        public string ImplementerCompany{ get; set; }

        [Property]
        public DateTime ChangeStartDateTime { get; set; }

        [Property]
        public DateTime ChangeEndDateTime { get; set; }

        [Property]
        public bool DoesChangeHaveDownTime { get; set; }

        [Property(Length = 6500)]
        public string Description { get; set; }

        [Property(Length = 6500)]
        public string Justification { get; set; }

        [Property(Length = 6500)]
        public string RollbackPlan { get; set; }

        [Property(Length = 50)]
        public string EnvironmentAffected { get; set; }

        [Property(Length = 6500)]
        public string ImpactOfNotImplementing { get; set; }

        [Property(Length = 50)]
        public string ChangeCategoryId{ get; set; }

        [Property(Length = 50)]
        public string ApprovalStatus { get; set; }

        [Property(Length = 50)]
        public string ApprovalReason { get; set; }

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
