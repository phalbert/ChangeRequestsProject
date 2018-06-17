using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class ChooseOtpMethod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    return;
                }
                string UserId = Request.QueryString["UserId"];
                ViewState["UserId"] = UserId;
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rdPhone.Checked && !rdEmail.Checked)
                {
                    //Show Error Message
                    string msg = "Please Select a Delivery Method";
                    lblMsg.Text = msg;
                    return;
                }

                //process
                string UserId = ViewState["UserId"] as string;
                string selected_method = rdPhone.Checked ? "PHONE" : "EMAIL";
                
                Response.Redirect("~/SupplyOTP.aspx?UserId=" + UserId + "&Method=" + selected_method);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }

        protected void rdBtnOTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdPhone.Checked = !rdEmail.Checked;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SupplyUsername.aspx");
        }
    }
}