using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChangeRequestSubSystem.ControlClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeRequestSubSystem.Entities;
using System.Data;

namespace ChangeRequestSubSystem.ControlClasses.Tests
{
    [TestClass()]
    public class ChangeRequestAPITests : AbstractModelTestCase
    {

        [TestMethod()]
        public void SendOneTimePINTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            SaveSystemUserTest();
            ApiResult result = api.SendOneTimePIN("kasozi.nsubuga@pegasus.co.ug", "PHONE");
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void SendOneTimePINTest_InvalidParameters()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ApiResult result = api.SendOneTimePIN(null, null);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
            Assert.IsNotNull(result.StatusDesc);
        }

        [TestMethod()]
        public void LoginTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            SaveSystemUserTest();
            OneTimePassword OTP = new OneTimePassword
            {
                Username = "kasozi.nsubuga@pegasus.co.ug",
                Password = "1234",
                ValidityDurationInSeconds=5*60*1000,
                CompanyCode="pegasus"
            };
            OTP.Save();
            SystemUser result = api.Login("kasozi.nsubuga@pegasus.co.ug", "1234");
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }



        [TestMethod()]
        public void LoginTest_InvalidParameters()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            SystemUser result = api.Login(null, null);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void UpdateChangeRequestStatusTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ApproverForChangeRequest link = new ApproverForChangeRequest();
            link.ChangeRequestId = "1234";
            link.Decision = "APPROVED";
            link.Reason = "APPROVED";
            link.ApproverId = "kasozi.nsubuga@pegasu.co.ug";
            ApiResult result = api.UpdateChangeRequestStatus(link);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void UpdateChangeRequestStatusTest_InvalidParameters()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            ApproverForChangeRequest link = new ApproverForChangeRequest();
            ApiResult result = api.UpdateChangeRequestStatus(link);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void AssignChangeRequestToApproverTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ApproverForChangeRequest link = new ApproverForChangeRequest();
            link.ChangeRequestId = "1234";
            link.Decision = "";
            link.Reason = "";
            link.ApproverId = "kasozi.nsubuga@pegasu.co.ug";
            ApiResult result = api.AssignChangeRequestToApprover(link);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void AssignChangeRequestToApproverTest_InvalidParameters()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            ApproverForChangeRequest link = new ApproverForChangeRequest();
            ApiResult result = api.AssignChangeRequestToApprover(link);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void ExecuteDataSetTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            DataTable result = api.ExecuteDataSet("GetSystemUserById", new object[] { "kasozi.nsubuga@pegasus.co.ug" }).Tables[0];
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void SaveChangeRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            changeRequest.ApprovalStatus = "PENDING";
            changeRequest.ApprovalReason = "";
            changeRequest.ChangeCategoryId = "EMERGENCY_CHANGE";
            changeRequest.ChangeRequestId = DateTime.Now.Ticks.ToString();
            changeRequest.ChangeEndDateTime = DateTime.Now;
            changeRequest.ChangeStartDateTime = DateTime.Now;
            changeRequest.ImpactOfNotImplementing = "Bad Stuff";
            changeRequest.ImplementerCompany = "Pegasus";
            changeRequest.ImplementerEmail = "kasozi.nsubuga@pegasus.co.ug";
            changeRequest.ImplementerName = "kasozi";
            changeRequest.ImplementerPhone = "0785975800";
            changeRequest.Justification = "We need to do this";
            changeRequest.RequesterCompany = "Stanbic";
            changeRequest.RequesterEmail = "damalie@stanbic.com";
            changeRequest.RequesterName = "Damalie";
            changeRequest.RequesterPhone = "0785975800";
            changeRequest.Title = "Changing Transaction Status";

            ApiResult result = api.SaveChangeRequest(changeRequest);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        //[TestMethod()]
        //public void TestStuff()
        //{

        //    OneTimePassword[] otp = OneTimePassword.QueryWithStoredProc2("GetLatestOTP","kasozi.nsubuga@pegasus.co.ug");
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void SaveCompanyTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            Company company = new Company();
            company.CompanyCode = "PEGASUS";
            company.CompanyName = "Pegasus Technologies Uganda";
            company.ModifiedBy = "Admin";
            ApiResult result = api.SaveCompany(company);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void SaveRoleTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            Role newRole = new Role();
            newRole.CompanyCode = "PEGASUS";
            newRole.RoleName = "Pegasus Team Lead";
            newRole.RoleCode = "REVIEWER";
            newRole.ModifiedBy = "Admin";
            ApiResult result = api.SaveRole(newRole);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void SaveSystemUserTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            SystemUser user = new SystemUser();
            user.CompanyCode = "PEGASUS";
            user.Username = "kasozi.nsubuga@pegasus.co.ug";
            user.RoleCode = "SUPER-ADMIN";
            user.ModifiedBy = "Admin";
            user.PhoneNumber = "256752001311";

            ApiResult result = api.SaveSystemUser(user);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void AttachSystemsAffectedToChangeRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            SystemAffected systemAffected = new SystemAffected();
            systemAffected.SystemName = "Test";
            systemAffected.SystemType = "Test";
            systemAffected.TypeOfChange = "Update";
            systemAffected.Details = "Test";
            ApiResult result = api.AttachSystemsAffectedToChangeRequest(systemAffected);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void AttachItemsToChangeRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            CR_Attachment systemAffected = new CR_Attachment();
            systemAffected.Base64StringOfContent = "Test";
            systemAffected.Name = "Test";
            systemAffected.Hash = "Update";
            ApiResult result = api.AttachItemsToChangeRequest(systemAffected);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void AttachPostChangeTestsToChangeRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            PostChangeTest systemAffected = new PostChangeTest();
            systemAffected.TestDesc = "Test";
            ApiResult result = api.AttachPostChangeTestToChangeRequest(systemAffected);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void AttachRollBackPlanToChangeRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            RollBackPlan systemAffected = new RollBackPlan();
            systemAffected.ChangeRequestId = "Test";
            systemAffected.RollBackPlanDetails = "Test";
          
            ApiResult result = api.AttachRollBackPlanToChangeRequest(systemAffected);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void SaveTimeBoundAccessRequestTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
            TimeBoundAccessRequest tbar = new TimeBoundAccessRequest();
            tbar.Approver = "nsubugak";
            tbar.ApproverReason = "PENDING";
            tbar.CreatedBy = "nsubugak";
            tbar.CreatedOn = DateTime.Now;
            tbar.DurationInMinutes = 15;
            tbar.ModifiedBy = "nsubugak";
            tbar.ModifiedOn = DateTime.Now;
            tbar.Reason = "To Debug Stuff";
            tbar.StartTime= DateTime.Now; ;
            tbar.Status = "PENDING";
            tbar.SystemCode = "DATABASE";
            tbar.TBPAccessId = DateTime.Now.Ticks.ToString();
            tbar.UserId = "nsubugak";
            tbar.TypeOfAccess = "UPDATE";


            ApiResult result = api.SaveTimeBoundAccessRequest(tbar);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }

        [TestMethod()]
        public void SendApproveCrEmailTest()
        {
            ChangeRequestInterface api = new CRSubSystemAPI();
           
            ChangeRequest changeRequest = new ChangeRequest();
            changeRequest.ApprovalStatus = "PENDING";
            changeRequest.ApprovalReason = "";
            changeRequest.ChangeCategoryId = "EMERGENCY_CHANGE";
            changeRequest.ChangeRequestId = DateTime.Now.Ticks.ToString();
            changeRequest.ChangeEndDateTime = DateTime.Now;
            changeRequest.ChangeStartDateTime = DateTime.Now;
            changeRequest.ImpactOfNotImplementing = "Bad Stuff";
            changeRequest.ImplementerCompany = "Pegasus";
            changeRequest.ImplementerEmail = "kasozi.nsubuga@pegasus.co.ug";
            changeRequest.ImplementerName = "kasozi";
            changeRequest.ImplementerPhone = "0785975800";
            changeRequest.Justification = "We need to do this";
            changeRequest.RequesterCompany = "Stanbic";
            changeRequest.RequesterEmail = "damalie@stanbic.com";
            changeRequest.RequesterName = "Damalie";
            changeRequest.RequesterPhone = "0785975800";
            changeRequest.Title = "Changing Transaction Status";
            changeRequest.Problem = "Pegasus server 192.168.55.3 wishes to access UTL airtime listening on URL http://172.25.100.6/atpurchase.php  in order to be able to send Airtime Top Up requests for UTL phone numbers";
            changeRequest.Solution = "Creation of UTL VPN";
            changeRequest.Save();

            ApproverForChangeRequest systemAffected = new ApproverForChangeRequest();
            systemAffected.ChangeRequestId = changeRequest.ChangeRequestId;
            systemAffected.ApproverId = "nsubugak";
            string ApproveURL = "";
            ApiResult result = api.SendApproveCrEmail(systemAffected);
            Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        }


        //[TestMethod()]
        //public void AttachRollBackPlanToChangeRequest()
        //{
        //    ChangeRequestInterface api = new ChangeRequestAPI();
        //    RiskAnalysis systemAffected = new RiskAnalysis();

        //    ApiResult result = api.AttachRiskAnalysisToChangeRequest(systemAffected);
        //    Assert.AreEqual(Globals.SUCCESS_STATUS_TEXT, result.StatusDesc);
        //}
    }
}