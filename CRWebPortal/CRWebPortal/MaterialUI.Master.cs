using CRWebPortal.CRSystemAPI;
using System;

namespace CRWebPortal
{
    public partial class MaterialUI : System.Web.UI.MasterPage
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
                    Response.Redirect("~/Default.aspx?Msg=Sorry, Session Has Expired and You have to Login again");
                    return;
                }
                if (Session["User"] == null)
                {
                    Response.Redirect("~/Default.aspx?Msg=Sorry, Session Has Expired and You have to Login again");
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