using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ChangeRequestSubSystem.ControlClasses;
using ChangeRequestSubSystem.Entities;

namespace ChangeRequestAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : CRSystemAPI
    {
        CRSubSystemAPI cRSubSystemAPI = new CRSubSystemAPI();

        public ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link) => cRSubSystemAPI.UpdateChangeRequestStatus(link);

        public ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link) => cRSubSystemAPI.AssignChangeRequestToApprover(link);

        public DataSet ExecuteDataSet(string StoredProc, object[] parameters) => cRSubSystemAPI.ExecuteDataSet(StoredProc, parameters);

        public int ExecuteNonQuery(string SqlQuery, TimeBoundAccessRequest tbar) => cRSubSystemAPI.ExecuteNonQuery(SqlQuery, tbar);

        public DataSet ExecuteSqlQuery(string SqlText, TimeBoundAccessRequest tbar) => cRSubSystemAPI.ExecuteSqlQuery(SqlText, tbar);

        public SystemUser Login(string Username, string Password) => cRSubSystemAPI.Login(Username, Password);

        public ApiResult SaveChangeRequest(ChangeRequest ChangeRequest) => cRSubSystemAPI.SaveChangeRequest(ChangeRequest);

        public ApiResult SaveCompany(Company req) => cRSubSystemAPI.SaveCompany(req);

        public ApiResult SaveSystemUser(SystemUser req) => cRSubSystemAPI.SaveSystemUser(req);

        public ApiResult SaveRole(Role req) => cRSubSystemAPI.SaveRole(req);

        public ApiResult SendOneTimePIN(string Username, string MethodOfSending) => cRSubSystemAPI.SendOneTimePIN(Username, MethodOfSending);

        public ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected) => cRSubSystemAPI.AttachSystemsAffectedToChangeRequest(systemAffected);

        public ApiResult AttachRollBackPlanToChangeRequest(RollBackPlan plan) => cRSubSystemAPI.AttachRollBackPlanToChangeRequest(plan);

        public ApiResult AttachItemsToChangeRequest(CR_Attachment attachment) => cRSubSystemAPI.AttachItemsToChangeRequest(attachment);

        public ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test) => cRSubSystemAPI.AttachPostChangeTestToChangeRequest(test);

        public ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis) => cRSubSystemAPI.AttachRiskAnalysisToChangeRequest(riskAnalysis);

        public ApiResult SaveTimeBoundAccessRequest(TimeBoundAccessRequest req) => cRSubSystemAPI.SaveTimeBoundAccessRequest(req);

        public TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user) => cRSubSystemAPI.CheckForValidTimeBoundAccessRequest(user);

        public string ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision) => cRSubSystemAPI.ApproveChangeRequest(UserId, ChangeRequestId, Decision).StatusDesc;

        public string ApproveTBAR(string UserId, string TbarId, string Decision) => cRSubSystemAPI.ApproveTBAR(UserId, TbarId, Decision).StatusDesc;

        public ApiResult SavePegasusSystem(PegasusSystem req) => cRSubSystemAPI.SavePegasusSystem(req);
    }
}
