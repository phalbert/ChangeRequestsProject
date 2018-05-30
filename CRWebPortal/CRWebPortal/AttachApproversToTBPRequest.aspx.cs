using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class AttachApproversToTBPRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Finished.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}