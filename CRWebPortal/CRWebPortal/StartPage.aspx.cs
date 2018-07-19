using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class StartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SystemUser user = Session["User"] as SystemUser;
                if (user == null)
                {
                    return;
                }
                lblUserRole.Text = user.RoleCode;
                lblFullName.Text = user.FullName;
            }
            catch (Exception)
            {
            }
        }
    }
}