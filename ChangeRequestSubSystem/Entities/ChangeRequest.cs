using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    public class ChangeRequest:ApiRequest
    {
        public string ChangeRequestId { get; set; }
        public string Title { get; set; }
        public string RequesterId { get; set; }
        public string RequesterAddress { get; set; }
        public string RequesterEmail { get; set; }
        public string RequesterPhone { get; set; }
        public string Implementer { get; set; }
        public string ImplementerEmail { get; set; }
        public string ImplementerPhone { get; set; }
        public string ImplementationDate { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string ImpactOfNotImplementing { get; set; }
        public string ChangeCategoryCode { get; set; }

        internal bool IsValidAssignRequest()
        {
            if (string.IsNullOrEmpty(ChangeRequestId))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = $"Please Supply a Username";
                return false;
            }

            return true;
        }

        internal bool IsValidUpdateRequest()
        {
            if (string.IsNullOrEmpty(ChangeRequestId))
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = $"Please Supply a Username";
                return false;
            }

            return true;
        }
    }
}
