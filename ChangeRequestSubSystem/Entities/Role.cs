using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class Role:ApiRequest
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
    }
}
