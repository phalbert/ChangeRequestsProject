using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class ApiRequest:BaseClass
    {
        public string ModifiedBy { get; set; }
        public string CompanyCode { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
