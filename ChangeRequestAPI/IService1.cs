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
        ApiResult AssignChangeRequestToApprover(ApproverToChangeRequestLink link);

        [OperationContract]
        ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link);

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
        TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user);

        [OperationContract]
        string ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision);

        [OperationContract]
        string ApproveTBAR(string UserId, string TbarId, string Decision);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
