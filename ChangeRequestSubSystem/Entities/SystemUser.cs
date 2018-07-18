using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("SystemUsers")]
    public class SystemUser:DbEntity<SystemUser>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length =50)]
        public string Email { get; set; }

        [Property(Length = 50)]
        public string PhoneNumber { get; set; }

        [Property(Length = 100)]
        public string FullName { get; set; }

        [Property(Length = 50, Unique = true)]
        public string Username { get; set; }

        [Property(Length = 50)]
        public string CompanyCode { get; set; }

        [Property(Length = 50)]
        public string RoleCode { get; set; }

        [Property(Length = 50)]
        public string DomainAccountUsername { get; set; }

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
            if (!SharedCommons.SharedCommons.IsValidEmail(Email))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = "PLEASE SUPPLY A VALID EMAIL";
                return false;
            }

            return base.IsValid();
        }
    }
}
