using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class SystemUser:ApiRequest
    {
        public string CompanyEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string OneTimePIN { get; set; }
        public string Username { get; set; }
        public string CompanyCode { get; set; }
        public string RoleCode { get; set; }
    }
}
