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

        ApiResult AttachItemsToChangeRequest(CR_Attachment attachment);

        ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected);

        ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test);

        ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis);

        //ApiResult SaveTBPARequest(ChangeRequest changeRequest);

        ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link);

        ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link);

        ApiResult SendOneTimePIN(string Username, string MethodOfSending);

        SystemUser Login(string Username, string Password);

        DataSet ExecuteDataSet(string StoredProc, object[] parameters);

    }

    public class ChangeRequestAPI : ChangeRequestInterface
    {
        static BussinessLogic bll = new BussinessLogic();

        public static ApiResult Initialize() => bll.Initialize();

        public static ApiResult DropAndRecreate() => bll.DropAndRecreate();

        public ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link) => bll.UpdateChangeRequestStatus(link);

        public ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link) => bll.AssignChangeRequestToApprover(link);

        public DataSet ExecuteDataSet(string StoredProc, object[] parameters) => bll.ExecuteDataSet(StoredProc, parameters);

        public SystemUser Login(string Username, string Password) => bll.Login(Username, Password);

        public ApiResult SaveChangeRequest(ChangeRequest ChangeRequest) => bll.SaveChangeRequest(ChangeRequest);

        public ApiResult SaveCompany(Company req) => bll.SaveCompany(req);

        public ApiResult AttachItemToChangeRequest(CR_Attachment req) => bll.AttachItemToChangeRequest(req);

        public ApiResult SaveRole(Role req) => bll.SaveRole(req);

        public ApiResult SendOneTimePIN(string Username, string MethodOfSending) => bll.SendOneTimePIN(Username, MethodOfSending);

        public ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected) => bll.AttachSystemAffectedToChangeRequest(systemAffected);

        public ApiResult AttachItemsToChangeRequest(CR_Attachment attachment) => bll.AttachItemToChangeRequest(attachment);

        public ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test) => bll.AttachPostChangeTestsToChangeRequest(test);

        public ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis) => bll.AttachRiskAnalysisToChangeRequest(riskAnalysis);
        
    }
}
