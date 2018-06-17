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
            SystemUser user = Session["User"] as SystemUser;
            TimeBoundAccessRequest tbar = BussinessLogic.cRSystemAPIClient.CheckForValidTimeBoundAccessRequest(user);

            if (tbar.StatusCode != Globals.SUCCESS_STATUS_CODE)
            {
                //Show Error Message
                Master.ErrorMessage = "ERROR:" + tbar.StatusDesc;
                return false;
            }

            string statementsAllowed = GetStatementsAllowed(tbar);
            string msg = $"SUCCESS: STATEMENTS YOU CAN EXECUTE {statementsAllowed}";
            Session["TBAR"] = tbar;

            LoadAutoCompleteIntellisense(tbar);
            Master.ErrorMessage = msg;
            return true;
        }

        private void LoadAutoCompleteIntellisense(TimeBoundAccessRequest tbar)
        {
            string GetAllTablesSql = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '{tbar.SystemCode}'";
            DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteSqlQuery(GetAllTablesSql, tbar).Tables[0];
            string array = "";
            foreach (DataRow dr in dt.Rows)
            {
                string tableName = dr[0].ToString();
                array += $"'{tableName}',";
            }
            array = array.TrimEnd(',');
            Session["Tables"] = array;
        }

        private string GetStatementsAllowed(TimeBoundAccessRequest request)
        {
            string statements = $"";
            switch (request.TypeOfAccess.ToUpper())
            {
                case "UPDATE":
                    statements = $"[SELECT's, UPDATE's] ON DB {request.SystemCode}";
                    break;
                case "DELETE":
                    statements = $"[SELECT's, UPDATE's, DELETE's] ON DB {request.SystemCode}";
                    break;
                case "INSERT":
                    statements = $"[SELECT's, UPDATE's, DELETE's,INSERT's] ON DB {request.SystemCode}";
                    break;
                case "CREATE":
                    statements = $"[SELECT's, UPDATE's, DELETE's,INSERT's,CREATE] ON DB {request.SystemCode}";
                    break;
                case "FULL":
                    statements = $"[ANY QUERY] ON DB {request.SystemCode}";
                    break;
                default:
                    statements= $"[SELECT's] ON DB {request.SystemCode}";
                    break;
            }
            return statements;
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
                    TimeBoundAccessRequest tbar = Session["TBAR"] as TimeBoundAccessRequest;
                    DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteSqlQuery(result.PegPayID, tbar).Tables[0];

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

                case "SELECT":
                    result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                    result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                    result.PegPayID = query;
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
                    result.PegPayID = queryType;
                    return result;
            }

        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {

                string query = txtQuery.Text;
                TimeBoundAccessRequest tbar = Session["TBAR"] as TimeBoundAccessRequest;
                int dt = BussinessLogic.cRSystemAPIClient.ExecuteNonQuery(query, tbar);


                //Show Error Message
                string msg1 = $"SUCCESS: {dt} ROWS WHERE AFFECTED.";
                Master.ErrorMessage = msg1;
                btnComplete.Visible = false;
                btnExecute.Visible = true;
                txtQuery.Enabled = true;
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