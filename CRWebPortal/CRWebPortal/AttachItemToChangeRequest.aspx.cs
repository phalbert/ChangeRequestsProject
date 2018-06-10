using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRWebPortal
{
    public partial class AttachItemToChangeRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    string crId = Request.QueryString["Id"];
                    ViewState["ChangeRequestId"] = crId;
                }
                if (IsPostBack)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
               
                CR_Attachment attachment = new CR_Attachment();

                string base64string = Getbase64StringOfFile();
                attachment.ChangeRequestId = Request.QueryString["Id"];
                attachment.CreatedBy = (Session["User"] as SystemUser)?.Username;
                attachment.CreatedOn = DateTime.Now;
                attachment.Name = fuAttachment.FileName;
                attachment.Base64StringOfContent = base64string;
                attachment.ModifiedBy = (Session["User"] as SystemUser)?.Username;
                attachment.ModifiedOn = DateTime.Now;
                attachment.Hash = "";

                ApiResult apiResult = BussinessLogic.cRSystemAPIClient.AttachItemsToChangeRequest(attachment);

                if (apiResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg = "ERROR:" + apiResult.StatusDesc;
                    Master.ErrorMessage = msg;
                    return;
                }

                DataTable dt = BussinessLogic.cRSystemAPIClient.ExecuteDataSet("GetItemsAttachedByChangeRequestId", new object[] { attachment.ChangeRequestId }).Tables[0];
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private string Getbase64StringOfFile()
        {

            if (fuAttachment.PostedFile.ContentLength == 0)
            {
                throw new Exception("Please Select a File to Upload");
            }


            Byte[] b = new byte[fuAttachment.PostedFile.ContentLength];
            fuAttachment.PostedFile.InputStream.Read(b, 0, b.Length);
            string base64String = System.Convert.ToBase64String(b, 0, b.Length);
            return base64String;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = ViewState["ChangeRequestId"] as string;
                Response.Redirect("~/AttachSystemsAffected.aspx?Id=" + crid);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                string crid = ViewState["ChangeRequestId"] as string;
                Response.Redirect("~/AttachApproversToCR.aspx?Id=" + crid);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }
    }
}