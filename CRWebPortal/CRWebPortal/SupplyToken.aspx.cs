using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class SupplyToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ExecuteQuery.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}