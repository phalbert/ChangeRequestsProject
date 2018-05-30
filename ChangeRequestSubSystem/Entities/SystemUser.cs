using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("SystemmUsers")]
    public class SystemUser:DbEntity<SystemUser>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length =50)]
        public string Email { get; set; }

        [Property(Length = 50)]
        public string PhoneNumber { get; set; }

        [Property(Length = 50, Unique = true)]
        public string Username { get; set; }

        [Property(Length = 50)]
        public string CompanyCode { get; set; }

        [Property(Length = 50)]
        public string RoleCode { get; set; }

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
