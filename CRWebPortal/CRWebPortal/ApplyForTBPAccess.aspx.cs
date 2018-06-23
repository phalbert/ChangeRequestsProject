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
    public partial class ApplyForTBPAccess : System.Web.UI.Page
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
            BussinessLogic.LoadDataIntoDropDown("GetSystemsForDropDown", ddDatabases, Session["User"] as SystemUser);
            BussinessLogic.LoadDataIntoDropDown("GetApproversForDropDown", ddApprover, Session["User"] as SystemUser);

            string dateFormat = "yyyy-MM-dd HH:mm";
            txtStartDateTime.Text = DateTime.Now.ToString(dateFormat);
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                TimeBoundAccessRequest req = new TimeBoundAccessRequest();

                string dateFormat = "yyyy-MM-dd HH:mm";
                req.Approver = ddApprover.SelectedValue;
                req.CreatedBy = (Session["User"] as SystemUser)?.Username;
                req.CreatedOn = DateTime.Now;
                req.ApproverReason = "PENDING";
                req.Status = "PENDING";
                req.DurationInMinutes = int.Parse(ddDuration.SelectedValue);
                req.Reason = txtReason.Text;
                req.StartTime = DateTime.ParseExact(txtStartDateTime.Text, dateFormat, CultureInfo.InvariantCulture);
                req.SystemCode = ddDatabases.SelectedValue;
                req.TypeOfAccess = ddTypeOfAccess.SelectedValue;
                req.UserId = (Session["User"] as SystemUser)?.Username;
                req.TBPAccessId = BussinessLogic.GenerateUniqueId("TBPA-");
                req.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                req.ModifiedOn = DateTime.Now;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.SaveTimeBoundAccessRequest(req);

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

        protected void ddDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

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