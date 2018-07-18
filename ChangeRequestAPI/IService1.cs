using ChangeRequestSubSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ChangeRequestAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface CRSystemAPI
    {
        // TODO: Add your service operations here
        [OperationContract]
        ApiResult SaveChangeRequest(ChangeRequest ChangeRequest);

        [OperationContract]
        ApiResult SaveCompany(Company req);

        [OperationContract]
        ApiResult SaveRole(Role req);

        [OperationContract]
        ApiResult SaveSystemUser(SystemUser req);

        [OperationContract]
        ApiResult AttachItemsToChangeRequest(CR_Attachment attachment);

        [OperationContract]
        ApiResult AttachSystemsAffectedToChangeRequest(SystemAffected systemAffected);

        [OperationContract]
        ApiResult AttachPostChangeTestToChangeRequest(PostChangeTest test);

        [OperationContract]
        ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis);

        [OperationContract]
        ApiResult AttachRollBackPlanToChangeRequest(RollBackPlan plan);

        [OperationContract]
        ApiResult AssignChangeRequestToApprover(ApproverForChangeRequest link);

        [OperationContract]
        ApiResult UpdateChangeRequestStatus(ApproverForChangeRequest link);

        [OperationContract]
        ApiResult SendOneTimePIN(string Username, string MethodOfSending);

        [OperationContract]
        SystemUser Login(string Username, string Password);

        [OperationContract]
        DataSet ExecuteDataSet(string StoredProc, object[] parameters);

        [OperationContract]
        ApiResult SaveTimeBoundAccessRequest(TimeBoundAccessRequest req);
       
        [OperationContract]
        DataSet ExecuteSqlQuery(string SqlText, TimeBoundAccessRequest tbar);

        [OperationContract]
        int ExecuteNonQuery(string SqlText, TimeBoundAccessRequest tbar);

        [OperationContract]
        TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user, string typeOfAccess);

        [OperationContract]
        string ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision, string Reason);

        [OperationContract]
        string ApproveTBAR(string UserId, string TbarId, string Decision,string Reason);

        [OperationContract]
        ApiResult SavePegasusSystem(PegasusSystem req);

        [OperationContract]
        ApiResult SaveGoLiveRequest(GoLiveRequest goliveRequest);
    }  
}
