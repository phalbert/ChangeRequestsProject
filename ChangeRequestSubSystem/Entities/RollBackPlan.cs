using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("RollBackPlans")]
    public class RollBackPlan : DbEntity<RollBackPlan>
    {

        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string RollBackPlanDetails { get; set; }
    }
}
