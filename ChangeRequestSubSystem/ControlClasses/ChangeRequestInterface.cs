using ChangeRequestSubSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.ControlClasses
{
   
    public interface ChangeRequestInterface
    {
        ApiResult SaveChangeRequest(ChangeRequest ChangeRequest);

        ApiResult SaveCompany(Company req);

        ApiResult SaveRole(Role req);

        ApiResult SaveSystemUser(SystemUser req);

        ApiResult AttachItemsToChangeRequest(CR_Attachment attachment);

        ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected);

        ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test);

        ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis);

        ApiResult AttachRollBackPlanToChangeRequest(RollBackPlan plan);

        ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link);

        ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link);

        ApiResult SendOneTimePIN(string Username, string MethodOfSending);

        SystemUser Login(string Username, string Password);

        DataSet ExecuteDataSet(string StoredProc, object[] parameters);

        ApiResult SaveTimeBoundAccessRequest(TimeBoundAccessRequest tbar);

        DataSet ExecuteSqlQuery(string SqlQuery, TimeBoundAccessRequest tbar);

        int ExecuteNonQuery(string SqlQuery, TimeBoundAccessRequest tbar);

        TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user);

        ApiResult SendApproveCrEmail(ApproverToChangeRequestLink link);

        ApiResult SendApproveTbarEmail(TimeBoundAccessRequest tbar);

        ApiResult SaveSystemSetting(SystemSetting req);

        ApiResult ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision);

        ApiResult ApproveTBAR(string UserId, string TbarId, string Decision);

    }

    public class CRSubSystemAPI : ChangeRequestInterface
    {
        static BussinessLogic bll = new BussinessLogic();

        public static ApiResult Initialize() => bll.Initialize();

        public static ApiResult DropAndRecreate() => bll.DropAndRecreate();

        public ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link) => bll.UpdateChangeRequestStatus(link);

        public ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link) => bll.AssignApproverToChangeRequest(link);

        public TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user) => bll.CheckForValidTimeBoundAccessRequest(user);

        public DataSet ExecuteDataSet(string StoredProc, object[] parameters) => bll.ExecuteDataSet(StoredProc, parameters);

        public DataSet ExecuteSqlQuery(string SqlQuery, TimeBoundAccessRequest tbar) => bll.ExecuteSqlQuery(SqlQuery, tbar);

        public int ExecuteNonQuery(string SqlQuery, TimeBoundAccessRequest tbar) => bll.ExecuteNonQuery(SqlQuery, tbar);

        public SystemUser Login(string Username, string Password) => bll.Login(Username, Password);

        public ApiResult SaveChangeRequest(ChangeRequest ChangeRequest) => bll.SaveChangeRequest(ChangeRequest);

        public ApiResult SaveCompany(Company req) => bll.SaveCompany(req);

        public ApiResult SaveTimeBoundAccessRequest(TimeBoundAccessRequest req) => bll.SaveTimeBoundAccessRequest(req);

        public ApiResult SaveSystemUser(SystemUser req) => bll.SaveSystemUser(req);

        public ApiResult SaveSystemSetting(SystemSetting req) => bll.SaveSystemSetting(req);

        public ApiResult SaveRole(Role req) => bll.SaveRole(req);

        public ApiResult SendOneTimePIN(string Username, string MethodOfSending) => bll.SendOneTimePIN(Username, MethodOfSending);

        public ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected) => bll.AttachSystemAffectedToChangeRequest(systemAffected);

        public ApiResult AttachRollBackPlanToChangeRequest(RollBackPlan plan) => bll.AttachRollBackPlanToChangeRequest(plan);

        public ApiResult AttachItemsToChangeRequest(CR_Attachment attachment) => bll.AttachItemToChangeRequest(attachment);

        public ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test) => bll.AttachPostChangeTestsToChangeRequest(test);

        public ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis) => bll.AttachRiskAnalysisToChangeRequest(riskAnalysis);

        public ApiResult SendApproveCrEmail(ApproverToChangeRequestLink link) => bll.SendApproveCrEmail(link);

        public ApiResult ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision) => bll.ApproveChangeRequest(UserId, ChangeRequestId, Decision);

        public ApiResult ApproveTBAR(string UserId, string TbarId, string Decision) => bll.ApproveTBAR(UserId, TbarId, Decision);

        public ApiResult SendApproveTbarEmail(TimeBoundAccessRequest tbar) => bll.SendApproveTbarEmail(tbar);
    }
}
