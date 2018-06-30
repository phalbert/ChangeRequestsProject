using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Data;
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
            
            BussinessLogic.LoadDataIntoDropDown("GetApproversForDropDown", ddApprover, Session["User"] as SystemUser);
            BussinessLogic.LoadDataIntoDropDown("GetSystemTypesForDropDown", ddSystemTypes, Session["User"] as SystemUser);
            BussinessLogic.LoadDataIntoDropDown("GetDatabasesForDropDown", ddSystems, Session["User"] as SystemUser);
            BussinessLogic.LoadDataIntoDropDown("GetDatabaseAccessTypesForDropDown", ddTypeOfAccess, Session["User"] as SystemUser);
            txtStartDateTime.Text = DateTime.Now.ToString(Globals.DATE_TIME_FORMAT);
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
                req.SystemCode = ddSystems.SelectedValue;
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

        protected void ddSystemTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string systemType = ddSystemTypes.SelectedValue.ToUpper();

                if (systemType == "DATABASE")
                {
                    BussinessLogic.LoadDataIntoDropDown("GetDatabasesForDropDown", ddSystems, Session["User"] as SystemUser);
                    BussinessLogic.LoadDataIntoDropDown("GetDatabaseAccessTypesForDropDown", ddTypeOfAccess, Session["User"] as SystemUser);
                    return;
                }
                else if (systemType == "SERVER")
                {
                    BussinessLogic.LoadDataIntoDropDown("GetServersForDropDown", ddSystems, Session["User"] as SystemUser);
                    BussinessLogic.LoadDataIntoDropDown("GetServerAccessTypesForDropDown", ddTypeOfAccess, Session["User"] as SystemUser);
                    return;
                }

                //Show Error Message
                ddSystems.Items.Clear();
                ddTypeOfAccess.Items.Clear();
                
                string msg = "ERROR: SYSTEM TYPE SPECIFIED IS NOT YET SUPPORTED FOR TBAR";
                Master.ErrorMessage = msg;
                return;
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