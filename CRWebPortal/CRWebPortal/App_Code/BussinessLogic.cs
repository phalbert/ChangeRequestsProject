using System;
using System.Data;
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

       
        internal static string GenerateUniqueId(string v)
        {
            try
            {
                return v + DateTime.Now.Ticks.ToString();
            }
            catch (Exception)
            {
                return v+"UNKNOWN";
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