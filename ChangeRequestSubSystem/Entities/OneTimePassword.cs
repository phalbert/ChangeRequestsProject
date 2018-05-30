using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("OTPs")]
    public class OneTimePassword:DbEntity<OneTimePassword>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string Username { get; set; }

        [Property(Length = 50)]
        public string Password { get; set; }

        [Property(Length = 50)]
        public string CompanyCode { get; set; }

        [Property(Length = 50)]
        public DateTime CreatedOn { get; set; }

        [Property(Length = 50)]
        public DateTime ValidityDurationInSeconds { get; set; }
    }
}
