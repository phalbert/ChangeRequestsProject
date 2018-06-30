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
            Session["TBAR"] = tbar;
            imgDiv.Visible = false;
            processingDiv.Visible = true;
        }

        protected void btnCloseWndw_Click(object sender, EventArgs e)
        {
            imgDiv.Visible = false;
            processingDiv.Visible = true;
            KillRDPSession();
            Response.Redirect("~/ChooseTbarMethod.aspx");
        }

        private void KillRDPSession()
        {
            try
            {
                Application app = Session["APP"] as Application;
                app?.Kill();
                Desktop.Instance.Windows().ForEach(i => 
                {
                    if (i.Title.ToUpper().Contains("TEAMVIEWER"))
                    {
                        i.Close();
                    } }
                );
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
                KillRDPSession();
                TimeBoundAccessRequest tbar = Session["TBAR"] as TimeBoundAccessRequest;
                CoreAppXmlConfiguration.Instance.BusyTimeout = 20000;
                Application app = TestStack.White.Application.Launch(@"E:\Software\TeamViewer_Setup.exe");

                TestStack.White.UIItems.WindowItems.Window tvWindow = GetTeamViewerWindow(app);
                TestStack.White.UIItems.RadioButton runOnceCheckBox = tvWindow?.Items.FirstOrDefault(w => w.Name.ToUpper().Contains("ONE TIME")) as TestStack.White.UIItems.RadioButton;
                runOnceCheckBox.Select();
                TestStack.White.UIItems.Button button = tvWindow?.Items.FirstOrDefault(w => w.Name.ToUpper().Contains("ACCEPT")) as TestStack.White.UIItems.Button;
                button.Click();
                tvWindow = GetTeamViewerWindow(app);

                //give tv some seconds to intialize
                Thread.Sleep(new TimeSpan(0, 0, 10));

                //Takes a screenshot of the entire desktop, and saves it to disk
                tvWindow.Focus(TestStack.White.UIItems.WindowItems.DisplayState.Maximized);
                Bitmap bitmap = Desktop.CaptureScreenshot();
                string ImageName = DateTime.Now.Ticks.ToString() + ".jpg";
                String saveImagePath = Server.MapPath("Images/") + ImageName;
                bitmap.Save(saveImagePath);
                tvImageDesktop.ImageUrl = "Images/" + ImageName;
                Session["APP"] = app;

                imgDiv.Visible = true;
                processingDiv.Visible = false;
                btnOpenWndw.Text = "RE-START THIS SESSION";

                Task.Factory.StartNew(() =>
                { 
                    DateTime maxDate = tbar.StartTime.AddMinutes(tbar.DurationInMinutes);
                    DateTime currentDate = DateTime.Now;
                    int minutesLeft = ((int)maxDate.Subtract(currentDate).TotalMinutes) + 1;//add an extra minute for network latency
                    Thread.Sleep(new TimeSpan(0, minutesLeft + AnyAdditionalMinutes, 0));
                    KillRDPSession();
                    //string msg = "SESSION HAS EXPIRED";
                    //Master.ErrorMessage = msg;
                    return;
                });
            
            }
            catch (Exception ex)
            {
                //Show Error Message
                string msg = "ERROR:" + ex.Message;
                Master.ErrorMessage = msg;
                return;
            }
        }

        private static TestStack.White.UIItems.WindowItems.Window GetTeamViewerWindow(Application app)
        {
            TestStack.White.UIItems.WindowItems.Window window = Desktop.Instance.Windows().FirstOrDefault(w => w.Title.ToUpper().Contains("TEAMVIEWER"));
            if (window == null)
            {
                Thread.Sleep(new TimeSpan(0, 0, 5));
                return GetTeamViewerWindow(app);
            }
            return window;
        }

        protected void btnCancelLoad_Click(object sender, EventArgs e)
        {

        }

        protected void btnPoll_Click(object sender, EventArgs e)
        {

        }
    }
}