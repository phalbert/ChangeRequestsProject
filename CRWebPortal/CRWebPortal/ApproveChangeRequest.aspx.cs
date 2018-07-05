using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class ApproveChangeRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string UserId = Request.QueryString["UserId"];
                string ChangeRequestId = Request.QueryString["ChangeRequestId"];
                string Decision = Request.QueryString["Decision"];
                string Reason = Request.QueryString["Reason"];

                if (Decision != "APPROVED")
                {
                    lblMsg.Text = "Hi, Please supply a detailed reason for rejection inorder to help the Requestor";
                    MultiView1.SetActiveView(ReasonView);
                    return;
                }

                btnSave.Text = "Return";
                string opResult = BussinessLogic.cRSystemAPIClient.ApproveChangeRequest(UserId, ChangeRequestId, Decision,Reason);
                lblMsg.Text = opResult;
            }
            catch(Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string UserId = Request.QueryString["UserId"];
                string ChangeRequestId = Request.QueryString["ChangeRequestId"];
                string Decision = Request.QueryString["Decision"];
                string Reason = txtReason.Text;
                string opResult = BussinessLogic.cRSystemAPIClient.ApproveChangeRequest(UserId, ChangeRequestId, Decision, Reason);

                lblMsg.Text = opResult;

                if (opResult.Contains(Globals.SUCCESS_STATUS_TEXT))
                {
                    MultiView1.SetActiveView(EmptyView);
                    return;
                }

                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }
    }
}