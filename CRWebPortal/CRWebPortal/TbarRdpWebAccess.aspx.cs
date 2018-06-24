using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class TbarRdpWebAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                TimeBoundAccessRequest tbar = BussinessLogic.IsAccessRequestIsValid(Session,"SERVER");

                if (tbar.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    string msg = "ERROR:" + tbar.StatusDesc;
                    Master.ErrorMessage = msg;
                    Multiview1.ActiveViewIndex = 0;
                    return;
                }

                if (IsPostBack)
                {
                    return;
                }

                LoadData(tbar);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private void LoadData(TimeBoundAccessRequest tbar)
        {
            Session["TBAR"] = tbar;
        }

        protected void btnCloseWndw_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ChooseTbarMethod.aspx");
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