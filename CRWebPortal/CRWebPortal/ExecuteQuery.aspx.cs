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
    public partial class ExecuteQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }
                if (!IsAccessRequestIsValid())
                {
                    Multiview1.ActiveViewIndex = 0;
                    return;
                }

                btnComplete.Visible = false;
                btnExecute.Visible = true;
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private bool IsAccessRequestIsValid()
        {
            return true;
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnComplete.Visible)
                {
                    btnComplete.Visible = false;
                    btnExecute.Visible = true;
                    txtQuery.Text = "";
                    txtQuery.Enabled = true;
                    dataGridResults.DataSource = null;

                }
                else
                {
                    Response.Redirect("~/Finished.aspx?Id");
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

        //private void switchActiveButtons()
        //{
        //   btnComplete.Visible=!btn
        //}

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlText = txtQuery.Text;
                ApiResult result = ConvertToSelect();

                if (result.StatusCode == Globals.PARSE_ERROR_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + result.StatusDesc;
                    dataGridResults.DataSource = null;
                    dataGridResults.DataBind();
                    Master.ErrorMessage = msg;
                    return;
                }

                if (result.StatusCode == Globals.FAILURE_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + result.StatusDesc;
                    Master.ErrorMessage = msg;
                    dataGridResults.DataSource = null;
                    dataGridResults.DataBind();
                    return;
                }

                //he input an update or delete statement
                if (result.StatusCode == Globals.SUCCESS_STATUS_CODE)
                {
                    DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteSqlQuery(result.PegPayID).Tables[0];

                    if (dt.Rows.Count <= 0)
                    {
                        //Show Error Message
                        string msg1 = "ERROR: NO ROWS WILL BE AFFECTED. DOUBLE CHECK YOUR QUERY";
                        Master.ErrorMessage = msg1;
                        dataGridResults.DataSource = null;
                        dataGridResults.DataBind();
                        return;
                    }

                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();

                    string msg = $"SUCCESS: {dt.Rows.Count} ROWS WILL BE AFFECTED. PLEASE CONFIRM QUERY EXECUTION";
                    Master.ErrorMessage = msg;
                    btnComplete.Visible = true;
                    btnExecute.Visible = false;
                    txtQuery.Enabled = false;
                    return;
                }

                //this is most likely an Insert or create Statment
                string msg2 = $"SUCCESS: {result.PegPayID} DETECTED. PLEASE CONFIRM QUERY EXECUTION";
                Master.ErrorMessage = msg2;
                txtQuery.Enabled = false;
                btnComplete.Visible = true;
                btnExecute.Visible = false;
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR, Query Failed:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private ApiResult ConvertToSelect()
        {
            ApiResult result = new ApiResult();
            string query = txtQuery.Text;

            if (string.IsNullOrEmpty(query)) { throw new Exception("Please supply a Query"); }

            string[] queryParts = query.Split(' ');
            string queryType = queryParts[0];
            string selectStatement = "";
            string tableName = "";
            string whereCondition = "";
            string[] whereConditions = { };


            switch (queryType.ToUpper())
            {
                case "UPDATE":
                    tableName = queryParts[1];
                    whereConditions = query.Split(new string[] { "where" }, StringSplitOptions.RemoveEmptyEntries);
                    whereCondition = whereConditions.Length > 1 ? whereConditions[1] : "";
                    selectStatement = whereConditions.Length > 1 ? $"Select * from {tableName} where {whereCondition}" : $"Select * from {tableName}";
                    result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                    result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                    result.PegPayID = selectStatement;
                    return result;

                case "DELETE":
                    string fromClause = query.Split(new string[] { "from" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    tableName = fromClause.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                    whereConditions = query.Split(new string[] { "where" }, StringSplitOptions.RemoveEmptyEntries);
                    whereCondition = whereConditions.Length > 1 ? whereConditions[1] : "";
                    selectStatement = whereConditions.Length > 1 ? $"Select * from {tableName} where {whereCondition}" : $"Select * from {tableName}";
                    result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                    result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                    result.PegPayID = selectStatement;
                    return result;

                case "INSERT":
                    result.StatusCode = "500";
                    result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                    result.PegPayID = queryType;
                    return result;

                case "CREATE":
                    result.StatusCode = "500";
                    result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                    result.PegPayID = queryType;
                    return result;

                default:
                    result.StatusCode = "300";
                    result.StatusDesc = "Unable to Determine Query Type";
                    return result;
            }

        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {

                string query = txtQuery.Text;
                int dt = BussinessLogic.cRSystemAPIClient.ExecuteNonQuery(query);

                if (dt <= 0)
                {
                    //Show Error Message
                    string msg1 = "ERROR: NO ROWS WHERE BE AFFECTED. DOUBLE CHECK YOUR QUERY";
                    Master.ErrorMessage = msg1;
                    return;
                }
                else
                {
                    //Show Error Message
                    string msg1 = $"SUCCESS: {dt} ROWS WHERE BE AFFECTED.";
                    Master.ErrorMessage = msg1;
                    btnComplete.Visible = false;
                    btnExecute.Visible = true;
                    txtQuery.Enabled = true;
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
    }
}