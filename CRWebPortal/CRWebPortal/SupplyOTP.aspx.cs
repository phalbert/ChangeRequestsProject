using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class SupplyOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (IsPostBack)
                {
                    return;
                }

                //read 
                string user_id = Request.QueryString["UserId"];
                string method = Request.QueryString["Method"];

                //store
                ViewState["UserId"] = user_id;
                ViewState["Method"] = method;

                //process
                btnResend_Click(sender, e);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChooseOtpMethod.aspx");
        }



        protected void btnResend_Click(object sender, EventArgs e)
        {
            try
            {
                //process
                string user_id = ViewState["UserId"] as string;
                string selected_method = ViewState["Method"] as string;
                ApiResult result = BussinessLogic.cRSystemAPIClient.SendOneTimePIN(user_id, selected_method);


                if (result.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string error = result.StatusDesc;
                    lblMsg.Text = error;
                    return;
                }

                //Show Error Message
                string msg = result.StatusDesc;
                lblMsg.Text = msg;
                return;
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                lblMsg.Text = msg;
                return;
            }
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            try
            {

                //process
                string user_id = ViewState["UserId"] as string;
                string user_password = txtOTP.Text;
                SystemUser user = BussinessLogic.cRSystemAPIClient.Login(user_id, user_password);


                if (user.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = user.StatusDesc;
                    lblMsg.Text = msg;
                    return;
                }

                Session["User"] = user;
                Response.Redirect($"~/{Globals.START_PAGE}");
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