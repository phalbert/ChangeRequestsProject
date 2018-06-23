using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using CRWebPortal.CRSystemAPI;

namespace CRWebPortal
{
    public class BussinessLogic
    {
        public static CRSystemAPIClient cRSystemAPIClient = new CRSystemAPIClient();

        public static CRSystemAPIClient GetCRSystemAPIHandle()
        {
            return cRSystemAPIClient;
        }

        public static TimeBoundAccessRequest IsAccessRequestIsValid(HttpSessionState Session)
        {
            TimeBoundAccessRequest tbar = new TimeBoundAccessRequest();
            try
            {
                SystemUser user = Session["User"] as SystemUser;
                tbar = Session["TBAR"] as TimeBoundAccessRequest == null ? cRSystemAPIClient.CheckForValidTimeBoundAccessRequest(user): Session["TBAR"] as TimeBoundAccessRequest;

                if (tbar.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    return tbar;
                }

                ApiResult checkResult = CheckIfTbarIsStillValid(tbar);

                if (checkResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    Session["TBAR"] = null;
                    tbar.StatusCode = checkResult.StatusCode;
                    tbar.StatusDesc = checkResult.StatusDesc;
                    return tbar;
                }

                Session["TBAR"] = tbar;
                return tbar;
            }
            catch (Exception ex)
            {
                tbar.StatusCode = Globals.FAILURE_STATUS_CODE;
                tbar.StatusDesc = "UNABLE TO VERIFY TO TBAR AT THE MOMENT:" + ex.Message;
            }
            return tbar;
        }

        public static ApiResult CheckIfTbarIsStillValid(TimeBoundAccessRequest tbar)
        {
            ApiResult result = new ApiResult();
            string dateFormat = "yyyy-MM-dd HH:mm";

            if (DateTime.Now < tbar.StartTime)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "FAILED: EARLY T.B.A.R USE DETECTED. T.B.A.R FOUND IS MEANT TO BE USED FROM [{tbar.StartTime.ToString(dateFormat)}] FOR [" + tbar.DurationInMinutes.ToString() + "] MINUTES. THE CURRENT TIME IS " + DateTime.Now.ToString(dateFormat) + "]. PLEASE TRY AGAIN AT SPECIFIED START TIME";
                return result;
            }

            if (DateTime.Now > tbar.StartTime.AddMinutes(tbar.DurationInMinutes))
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "FAILED: TBAR HAS EXPIRED. START TIME WAS [" + tbar.StartTime.ToString(dateFormat) + "] FOR [" + tbar.DurationInMinutes.ToString() + "] MINUTES. CURRENT TIME IS [" + DateTime.Now.ToString(dateFormat) + "]";
                return result;
            }

            result.StatusCode = Globals.SUCCESS_STATUS_CODE;
            result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            return result;
        }

        internal static string GenerateUniqueId(string v)
        {
            try
            {
                return v + DateTime.Now.Ticks.ToString();
            }
            catch (Exception)
            {
                return v + "UNKNOWN";
            }
        }

        internal static void LoadDataIntoDropDown(string StoredProc, DropDownList ddlst, SystemUser systemUser)
        {
            string[] parameters = { systemUser.Username };
            DataSet ds = cRSystemAPIClient.ExecuteDataSet(StoredProc, parameters);
            DataTable dt = ds.Tables[0];

            ddlst.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                string Text = dr[0].ToString();
                string Value = dr[1].ToString();
                ddlst.Items.Add(new ListItem(Text, Value));
            }
        }
    }
}