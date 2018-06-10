using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class SiteMaster : System.Web.UI.MasterPage
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

        }
    }
}
