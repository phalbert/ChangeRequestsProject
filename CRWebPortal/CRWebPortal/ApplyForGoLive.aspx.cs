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
    public partial class ApplyForGoLive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private void LoadData()
        {

            BussinessLogic.LoadDataIntoDropDown("GetApproversForDropDown", ddApprover, Session["User"] as SystemUser);
         
            
            txtStartDateTime.Text = DateTime.Now.ToString(Globals.DATE_TIME_FORMAT);
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                GoLiveRequest req = new GoLiveRequest();

                req.CreatedBy = (Session["User"] as SystemUser)?.Username;
                req.CreatedOn = DateTime.Now;
                req.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                req.ModifiedOn = DateTime.Now;
                req.ApproverDecision = "PENDING";
                req.ApproverUserId = ddApprover.SelectedValue;
                req.ChangeRequestId = ddChangeRequests.SelectedValue;
                req.GoLiveStartDateTime = DateTime.ParseExact(txtStartDateTime.Text, Globals.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
                req.RequesterUserId= (Session["User"] as SystemUser)?.Username;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.SaveGoLiveRequest(req);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                Response.Redirect("~/Finished.aspx");
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