using ChangeRequestSubSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.ControlClasses
{
    public interface ChangeRequestInterface
    {
        ApiResult SaveChangeRequest(ChangeRequest ChangeRequest);

        ApiResult SaveCompany(Company req);

        ApiResult SaveRole(Role req);

        ApiResult AssignChangeRequestToApprover(ChangeRequest changeRequest, Approver approver);

        ApiResult UpdateChangeRequestStatus(Approver approver, ChangeRequest changeRequest, string Decision);

        ApiResult SendOneTimePIN(string Username, string MethodOfSending);

        SystemUser Login(string Username, string Password);

        DataSet ExecuteDataSet(string StoredProc, object[] parameters);
    }

    public class ChangeRequestAPI : ChangeRequestInterface
    {
        BussinessLogic bll = new BussinessLogic();

        public ApiResult UpdateChangeRequestStatus(Approver approver, ChangeRequest changeRequest,string Decision) => bll.UpdateChangeRequestStatus(approver, changeRequest, Decision);

        public ApiResult AssignChangeRequestToApprover(ChangeRequest changeRequest, Approver approver) => bll.AssignChangeRequestToApprover(changeRequest, approver);

        public DataSet ExecuteDataSet(string StoredProc, object[] parameters) => bll.ExecuteDataSet(StoredProc, parameters);

        public SystemUser Login(string Username, string Password) => bll.Login(Username, Password);

        public ApiResult SaveChangeRequest(ChangeRequest ChangeRequest) => bll.SaveChangeRequest(ChangeRequest);

        public ApiResult SaveCompany(Company req) => bll.SaveCompany(req);

        public ApiResult SaveRole(Role req) => bll.SaveRole(req);

        public ApiResult SendOneTimePIN(string Username, string MethodOfSending) => bll.SendOneTimePIN(Username, MethodOfSending);
    }
}
