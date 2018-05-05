using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class Approver : SystemUser
    {
        internal bool IsValidAssignRequest()
        {
            if (string.IsNullOrEmpty(Username))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = $"Please Supply a Username";
                return false;
            }

            return true;
        }

        internal bool IsValidUpdateRequest()
        {
            if (string.IsNullOrEmpty(Username))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = $"Please Supply a Username";
                return false;
            }

            return true;
        }
    }
}
