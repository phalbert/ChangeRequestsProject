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
    public class ChangeRequestAPITests
    {
        [TestMethod()]
        public void SendOneTimePINTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ApiResult result = api.SendOneTimePIN("kasozi.nsubuga@pegasus.co.ug", "PHONE");
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void SendOneTimePINTest_InvalidParameters()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ApiResult result = api.SendOneTimePIN(null, null);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
            Assert.IsNotNull(result.StatusDesc);
        }

        [TestMethod()]
        public void LoginTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            SystemUser result = api.Login("kasozi.nsubuga@pegasus.co.ug", "1234");
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void LoginTest_InvalidParameters()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            SystemUser result = api.Login(null, null);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void UpdateChangeRequestStatusTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            changeRequest.ChangeRequestId = "122333333";
            Approver approver = new Approver();
            approver.Username = "kasozi.nsubuga@pegasus.co.ug";
            string Decision = "APPROVED";
            ApiResult result = api.UpdateChangeRequestStatus(approver,changeRequest, Decision);
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void UpdateChangeRequestStatusTest_InvalidParameters()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            Approver approver = new Approver();
            ApiResult result = api.UpdateChangeRequestStatus(approver, changeRequest, "");
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void AssignChangeRequestToApproverTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            changeRequest.ChangeRequestId = "122333333";
            Approver approver = new Approver();
            approver.Username = "kasozi.nsubuga@pegasus.co.ug";
            ApiResult result = api.AssignChangeRequestToApprover(changeRequest,approver);
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void AssignChangeRequestToApproverTest_InvalidParameters()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            Approver approver = new Approver();
            ApiResult result = api.AssignChangeRequestToApprover(changeRequest, approver);
            Assert.AreEqual(Globals.FAILURE_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void ExecuteDataSetTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            DataTable result = api.ExecuteDataSet("GetSystemUserById",new object[] {"kasozi.nsubuga@pegasus.co.ug" }).Tables[0];
            Assert.IsNotNull(result);
        }
        

        [TestMethod()]
        public void SaveChangeRequestTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            ChangeRequest changeRequest = new ChangeRequest();
            changeRequest.ChangeCategoryCode = "N/A";
            changeRequest.ChangeRequestId = DateTime.Now.Ticks.ToString();
            changeRequest.CompanyCode = "PEGASUS";
            changeRequest.Description = "Test Desc";
            changeRequest.ImpactOfNotImplementing = "Serious";
            changeRequest.ImplementationDate = "2018-05-05";
            changeRequest.Implementer = "Kasozi";
            changeRequest.ImplementerEmail = "kasozi.nsubuga@pegasus.co.ug";
            changeRequest.ImplementerPhone = "0752001311";
            changeRequest.Justification = "Test";
            changeRequest.ModifiedBy = "kasozi.nsubuga@pegasus.co.ug";
            changeRequest.RequesterAddress = "Ntinda";
            changeRequest.RequesterEmail = "techsupport@pegasus.co.ug";
            changeRequest.RequesterId = "techsupport";
            changeRequest.RequesterPhone = "0752001311";
            changeRequest.Title = "Test Change Request";
            ApiResult result = api.SaveChangeRequest(changeRequest);
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void SaveCompanyTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            Company company = new Company();
            company.CompanyCode = "PEGASUS";
            company.CompanyName = "Pegasus Technologies Uganda";
            company.ModifiedBy = "Admin";
            ApiResult result = api.SaveCompany(company);
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }

        [TestMethod()]
        public void SaveRoleTest()
        {
            ChangeRequestInterface api = new ChangeRequestAPI();
            Role newRole = new Role();
            newRole.CompanyCode = "PEGASUS";
            newRole.RoleName = "Pegasus Team Lead";
            newRole.RoleCode = "REVIEWER";
            newRole.ModifiedBy = "Admin";
            ApiResult result = api.SaveRole(newRole);
            Assert.AreEqual(Globals.SUCCESS_STATUS_CODE, result.StatusCode);
        }
        
    }
}