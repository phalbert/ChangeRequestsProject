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
    public partial class CreateSystemUser : System.Web.UI.Page
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
            BussinessLogic.LoadDataIntoDropDown("GetRolesForDropDown", ddRoles, Session["User"] as SystemUser);
            LoadSystemUsers(Session["User"] as SystemUser);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SystemUser user = new SystemUser();
                user.CreatedBy = (Session["User"] as SystemUser)?.Username;
                user.CreatedOn = DateTime.Now;
                user.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                user.ModifiedOn = DateTime.Now;
                user.CompanyCode = "PEGASUS";
                user.DomainAccountUsername = txtDomainAccount.Text;
                user.Email = txtEmail.Text;
                user.FullName = txtFullName.Text;
                user.PhoneNumber = txtPhoneNumber.Text;
                user.RoleCode = ddRoles.SelectedValue;
                user.Username = txtUsername.Text;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.SaveSystemUser(user);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                LoadSystemUsers(user);

                string msg1 = "SUCCESS: User Saved Successfully";
                Master.ErrorMessage = msg1;
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

        private void LoadSystemUsers(SystemUser user)
        {
            DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("GetSystemUsersForDropDown", new object[] {user.ModifiedBy }).Tables[0];
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}