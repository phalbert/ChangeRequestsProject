using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("DbQueryLogs")]
    public class DbQueryLog:DbEntity<DbQueryLog>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int RecordId { get; set; }

        [Property]
        public string UserId { get; set; }

        [Property]
        public string QuerySql { get; set; }

        [Property]
        public string NumberOfRowsAffected { get; set; }

        [Property]
        public DateTime? RecordDate { get; set; }

        public bool SaveLog()
        {
            try
            {
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override bool IsValid()
        {
            string propertiesThatCanBeNull = $"{nameof(RecordId)}|{nameof(NumberOfRowsAffected)}";
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
