using ChangeRequestSubSystem.ControlClasses;
using ChangeRequestSubSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CRSubSystemAPI.Initialize();
            ChangeRequestInterface api = new CRSubSystemAPI();
            ApproverForChangeRequest link = new ApproverForChangeRequest
            {
                ChangeRequestId = "12345",
                Decision = "APPROVED",
                Reason = "APPROVED",
                ApproverId = "kasozi.nsubuga@pegasus.co.ug"
            };
            ApiResult result = api.UpdateChangeRequestStatus(link);
            Console.Read();
        }
    }
}
