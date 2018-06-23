using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class AttachApproversToCR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    string crId = Request.QueryString["Id"];
                    ViewState["ChangeRequestId"] = crId;
                }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                ApproverForChangeRequest attachment = new ApproverForChangeRequest();

                attachment.ChangeRequestId = Request.QueryString["Id"];
                attachment.CreatedBy = (Session["User"] as SystemUser)?.Username;
                attachment.CreatedOn = DateTime.Now;
                attachment.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                attachment.ModifiedOn = DateTime.Now;
                attachment.Decision = "PENDING";
                attachment.Reason = "PENDING";
                attachment.ApproverId = ddNameOfApprover.SelectedValue;
                attachment.Role = ddTypeOfApprover.SelectedValue;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.AssignChangeRequestToApprover(attachment);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("GetApproversByChangeRequestId", new object[] { attachment.ChangeRequestId }).Tables[0];
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = Request.QueryString["Id"];
                Response.Redirect("~/AttachApproversToCR.aspx?Id=" + crid);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = Request.QueryString["Id"];
                Response.Redirect("~/Finished.aspx?Id="+crid);
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