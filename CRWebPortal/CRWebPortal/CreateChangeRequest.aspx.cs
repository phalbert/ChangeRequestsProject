using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class CreateChangeRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string dateFormat = "yyyy-MM-dd HH:mm";
                ChangeRequest changeRequest = new ChangeRequest
                {
                    ApprovalStatus = "PENDING",
                    ApprovalReason = "",
                    ChangeCategoryId = ddChangeCategories.SelectedValue,
                    ChangeRequestId = BussinessLogic.GenerateUniqueId("CR-"),
                    ChangeEndDateTime = !string.IsNullOrEmpty(txtImplementationStartDate.Text)? DateTime.ParseExact(txtImplementationStartDate.Text, dateFormat, CultureInfo.InvariantCulture):throw new Exception("Please Supply a Start Date"),
                    ChangeStartDateTime = !string.IsNullOrEmpty(txtImplementationEndDateTime.Text) ? DateTime.ParseExact(txtImplementationEndDateTime.Text, dateFormat, CultureInfo.InvariantCulture) : throw new Exception("Please Supply a End Date"),
                    ImpactOfNotImplementing = txtImpact.Text,
                    ImplementerCompany = (Session["User"] as SystemUser)?.CompanyCode,
                    ImplementerEmail = txtImplementerEmail.Text,
                    ImplementerName = txtImplementerName.Text,
                    ImplementerPhone = txtImplementerPhone.Text,
                    Justification = txtJustification.Text,
                    RequesterCompany = txtReqAddress.Text,
                    RequesterEmail = txtReqEmail.Text,
                    RequesterName = txtReqName.Text,
                    RequesterPhone = txtReqPhone.Text,
                    Title = txtTitle.Text,
                    Problem=txtProblemDesc.Text,
                    Solution=txtSolutionDesc.Text
                };

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.SaveChangeRequest(changeRequest);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                Response.Redirect("~/AttachSystemsAffected.aspx?Id=" + changeRequest.ChangeRequestId);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }
    }
}