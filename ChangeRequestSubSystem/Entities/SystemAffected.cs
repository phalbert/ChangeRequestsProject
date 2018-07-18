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

        public override bool IsValid()
        {
            string propertiesThatCanBeNull = $"{nameof(Id)}";
            string nullCheckResult = SharedCommons.SharedCommons.CheckForNulls(this, propertiesThatCanBeNull);
            if (nullCheckResult != Globals.SUCCESS_STATUS_TEXT)
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = nullCheckResult;
                return false;
            }

            return base.IsValid();
        }
    }
}
