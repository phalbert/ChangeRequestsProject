using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class ApplyForTBPAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AttachApproversToTBPRequest.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}