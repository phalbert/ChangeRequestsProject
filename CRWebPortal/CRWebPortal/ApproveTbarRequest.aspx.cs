using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class ApproveTbarRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string UserId = Request.QueryString["UserId"];
                string TbarId = Request.QueryString["TbarId"];
                string Decision = Request.QueryString["Decision"];
                string Reason = Request.QueryString["Reason"];

                string opResult = BussinessLogic.cRSystemAPIClient.ApproveTBAR(UserId, TbarId, Decision);
                lblMsg.Text = opResult;
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }
    }
}