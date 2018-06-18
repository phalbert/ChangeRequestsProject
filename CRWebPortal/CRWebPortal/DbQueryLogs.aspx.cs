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
    public partial class DbQueryLogs : System.Web.UI.Page
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
            BussinessLogic.LoadDataIntoDropDown("GetSystemUsersForDropDown", ddUsers, Session["User"] as SystemUser);
            //BussinessLogic.LoadDataIntoDropDown("GetCompaniesForDropDown", ddCompany, Session["User"] as SystemUser);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SearchDB();
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private void SearchDB()
        {
            string dateFormat = "yyyy-MM-dd HH:mm";
            DateTime startDate = DateTime.ParseExact(txtStartDate.Text, dateFormat, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txtEndDate.Text, dateFormat, CultureInfo.InvariantCulture);
            string UserId = ddUsers.SelectedValue;
           
            object[] parameters = { UserId, startDate, endDate };

            DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("SearchDbQueryLogs",  parameters).Tables[0];
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            Multiview2.ActiveViewIndex = 0;
        }
    }
}