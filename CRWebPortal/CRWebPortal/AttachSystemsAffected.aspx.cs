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
    public partial class AttachSystemsAffected : System.Web.UI.Page
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
            BussinessLogic.LoadDataIntoDropDown("GetSystemsForDropDown", ddTypeOfSystem, Session["User"] as SystemUser);
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = ViewState["ChangeRequestId"] as string;
                Response.Redirect("~/CreateChangeRequest.aspx?Id=" + crid);
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
                SystemAffected systemAffected = new SystemAffected();
                systemAffected.ChangeRequestId = ViewState["ChangeRequestId"] as string;
                systemAffected.CreatedBy = (Session["User"] as SystemUser)?.Username;
                systemAffected.CreatedOn = DateTime.Now;
                systemAffected.Details = ddTypeOfSystem.SelectedValue;
                systemAffected.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                systemAffected.ModifiedOn = DateTime.Now;
                systemAffected.SystemName = ddTypeOfSystem.SelectedValue;
                systemAffected.SystemType = ddTypeOfSystem.SelectedValue;
                systemAffected.TypeOfChange = ddTypeOfChange.SelectedValue;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.AttachSystemsAffectedToChangeRequest(systemAffected);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("GetSystemsAffectedByChangeRequestId", new object[] {systemAffected.ChangeRequestId }).Tables[0];
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

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = ViewState["ChangeRequestId"] as string;
                Response.Redirect("~/AttachPostChangeTestsToCR.aspx?Id=" + crid);
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
