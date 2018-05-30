using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class ApiResult : BaseClass
    {
        public string PegPayID { get; set; }

        public virtual bool SetSuccessAsStatusInResponseFields()
        {
            StatusCode = Globals.SUCCESS_STATUS_CODE;
            StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            return true;
        }

        public virtual bool SetFailuresAsStatusInResponseFields(string Message)
        {
            StatusCode = Globals.FAILURE_STATUS_CODE;
            StatusDesc = "FAILED: " + Message;
            return true;
        }

      
    }
}
