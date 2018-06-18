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
    public partial class SaveSystem : System.Web.UI.Page
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
            BussinessLogic.LoadDataIntoDropDown("GetSystemTypesForDropDown", ddSysTypes, Session["User"] as SystemUser);
            txtSysCode.Text = BussinessLogic.GenerateUniqueId("SYS-");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PegasusSystem system = new PegasusSystem();
                system.CreatedBy = (Session["User"] as SystemUser)?.Username;
                system.CreatedOn = DateTime.Now;
                system.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                system.ModifiedOn = DateTime.Now;
                system.ConnectionString = txtConnectionString.Text;
                system.SystemCode = txtSysCode.Text;
                system.SystemName = txtSysName.Text;
                system.SystemType = ddSysTypes.SelectedValue;

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.SavePegasusSystem(system);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("GetSystemsForDropDown", new object[] {system.ModifiedBy }).Tables[0];
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();

                string msg1 = "SUCCESS: System Saved Successfully";
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

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}