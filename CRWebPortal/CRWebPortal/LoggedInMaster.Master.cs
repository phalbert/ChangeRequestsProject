using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class LoggedInMaster : System.Web.UI.MasterPage
    {
        public string ErrorMessage
        {
            get
            {
                return lblMsg.Text;
            }
            set
            {
                lblMsg.Text = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/Default.aspx?Msg=Sorry, You have to Login again");
                    return;
                }

                SystemUser user = Session["User"] as SystemUser;
                
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                ErrorMessage = msg;
                return;
            }
        }
    }
}