using CRWebPortal.CRSystemAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestStack.White;
using TestStack.White.Configuration;

namespace CRWebPortal
{
    public partial class TbarRdpWebAccess : System.Web.UI.Page
    {
        private const string ServiceName = "AnyDesk Service";
        private const string GroupName = "Domain Admins";
        private const string Domain = "PGSS";
        public int AnyAdditionalMinutes = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                TimeBoundAccessRequest tbar = BussinessLogic.IsAccessRequestIsValid(Session, "SERVER");

                if (tbar.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    string msg = "ERROR:" + tbar.StatusDesc;
                    Master.ErrorMessage = msg;
                    Multiview1.ActiveViewIndex = 0;
                    return;
                }

                if (IsPostBack)
                {
                    return;
                }

                LoadData(tbar);
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private void LoadData(TimeBoundAccessRequest tbar)
        {
            DateTime maxDate = tbar.StartTime.AddMinutes(tbar.DurationInMinutes);
            DateTime currentDate = DateTime.Now;
            int minutesLeft = ((int)maxDate.Subtract(currentDate).TotalMinutes) + 1;//add an extra minute for network latency
            Session[Globals.RDP_TBAR_SESSION_KEY_NAME] = tbar;
            PegasusSystem system = SimpleDatabaseHandler<PegasusSystem>.QueryWithStoredProc("GetSystemById", tbar.SystemCode).FirstOrDefault();

            if (system == null)
            {
                //Show Error Message
                string msg = "ERROR: SYSTEM WITH CODE " + tbar.SystemCode + " NOT FOUND";
                Master.ErrorMessage = msg;
                return;
            }

            string[] credentials = system.ConnectionString.Split(':');

            if (credentials.Length < 3)
            {
                //Show Error Message
                string msg = "ERROR: INCOMPLETE SYSTEM CREDENTIALS. EXPECTED 3. FOUND " + credentials.Length;
                Master.ErrorMessage = msg;
                return;
            }

            string IP = credentials[0];
            string Username = credentials[1];
            string Password = credentials[2];

            lblSystemName.Text = system.SystemName;
            lblUsername.Text = Username;
            lblPassword.Text = Password;
        }

        protected void btnCloseWndw_Click(object sender, EventArgs e)
        {
            try
            {
                TimeBoundAccessRequest tbar = Session["RDP_TBAR"] as TimeBoundAccessRequest;
                SystemUser user = Session["User"] as SystemUser;


                //lblSystemState.Text = "RDP SERVICE STARTED. USE ANY DESK FOR RDP";
                SharedCommonsAPI.SharedCommonsAPISoapClient sharedCommons = new SharedCommonsAPI.SharedCommonsAPISoapClient();

                SharedCommonsAPI.CommonResult result = sharedCommons.StopService(ServiceName);

                if (result.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg1 = "ERROR: UNABLE TO STOP RDP SESSIOn";
                    Master.ErrorMessage = msg1;
                    return;
                }

                Task.Factory.StartNew(() =>
                {
                    if (!user.RoleCode.ToUpper().Contains("ADMIN"))
                    {
                        result = sharedCommons.RemoveUserFromDomainGroup(user.DomainAccountUsername, GroupName, Domain);
                    }
                });

                //Show Error Message
                string msg = "SUCCESS: RDP SESSION CANCELLED.";
                Master.ErrorMessage = msg;
                Response.Redirect("~/ChooseTbarMethod.aspx");
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }


        protected void btnOpenWndw_Click(object sender, EventArgs e)
        {
            try
            {
                TimeBoundAccessRequest tbar = Session["RDP_TBAR"] as TimeBoundAccessRequest;
                SystemUser user = Session["User"] as SystemUser;

                //lblSystemState.Text = "RDP SERVICE STARTED. USE ANY DESK FOR RDP";
                SharedCommonsAPI.SharedCommonsAPISoapClient sharedCommons = new SharedCommonsAPI.SharedCommonsAPISoapClient();

                SharedCommonsAPI.CommonResult result = sharedCommons.StartService(ServiceName);

                if (result.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg1 = "ERROR: UNABLE TO START RDP SERVICE ";
                    Master.ErrorMessage = msg1;
                    return;
                }

             
                result = sharedCommons.AddUserToDomainGroup(user.DomainAccountUsername, GroupName, Domain);

                if (result.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    //Show Error Message
                    string msg1 = $"ERROR: RDP SERVICE STARTED,FAILED TO GIVE ADMIN ACCESS TO USER {user.DomainAccountUsername}. REASON {result.StatusDesc} ";
                    Master.ErrorMessage = msg1;
                    return;
                }

                Task.Factory.StartNew(() =>
                {
                    result = ScheduleServiceStop(tbar, user, sharedCommons, result);
                });

                //Show Error Message
                string msg2 = "SUCCESS: RDP SERVICE STARTED. USE ANY DESK FOR RDP AND USE THE DETAILS BELOW";
                Master.ErrorMessage = msg2;

            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private static SharedCommonsAPI.CommonResult ScheduleServiceStop(TimeBoundAccessRequest tbar, SystemUser user, SharedCommonsAPI.SharedCommonsAPISoapClient sharedCommons, SharedCommonsAPI.CommonResult result)
        {
            try
            {
                DateTime maxDate = tbar.StartTime.AddMinutes(tbar.DurationInMinutes);
                DateTime currentDate = DateTime.Now;
                int minutesLeft = ((int)maxDate.Subtract(currentDate).TotalMinutes) + 1;//add an extra minute for network latency
                sharedCommons.StopServiceAfterXminutesAsync(ServiceName, minutesLeft);

                currentDate = DateTime.Now;
                minutesLeft = ((int)maxDate.Subtract(currentDate).TotalMinutes);

                Task.Factory.StartNew(() =>
                {
                    if (!user.RoleCode.ToUpper().Contains("ADMIN"))
                    {
                        Thread.Sleep(new TimeSpan(0, minutesLeft, 0));
                        result = sharedCommons.RemoveUserFromDomainGroup(user.DomainAccountUsername, GroupName, Domain);
                    }
                });
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        private static SharedCommonsAPI.CommonResult RemoveUser(SystemUser user, SharedCommonsAPI.SharedCommonsAPISoapClient sharedCommons, SharedCommonsAPI.CommonResult result, int minutesLeft)
        {
            Thread.Sleep(new TimeSpan(0, minutesLeft, 0));
            if (!user.RoleCode.ToUpper().Contains("ADMIN"))
            {
                result = sharedCommons.RemoveUserFromDomainGroup(user.DomainAccountUsername, GroupName, Domain);
            }

            return result;
        }

        protected void btnCancelLoad_Click(object sender, EventArgs e)
        {

        }

        protected void btnPoll_Click(object sender, EventArgs e)
        {

        }
    }
}