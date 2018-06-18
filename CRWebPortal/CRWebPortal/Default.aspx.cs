using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string Msg = Request.QueryString["Msg"];

                if (Msg == null)
                {
                    Response.Redirect("~/SupplyUsername.aspx");
                    return;
                }

                msg.Text = Msg;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/SupplyUsername.aspx");
            }
        }
    }
}
