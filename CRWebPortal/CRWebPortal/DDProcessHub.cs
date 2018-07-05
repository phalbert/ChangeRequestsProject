using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using CRWebPortal;
using CRWebPortal.CRSystemAPI;
using TestStack.White.Configuration;
using TestStack.White;
using System.Drawing;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;

namespace Program.Hubs
{
    [HubName("DDProcess")]
    public class DDProcessHub : Hub
    {
        public string msg = "Initializing and Preparing...";
        public int count = 100;

        public void CallLongOperation()
        {
            StartRDPSession(Clients);
        }

        private void StartRDPSession(IHubCallerConnectionContext<dynamic> clients)
        {
            string msg = "Loading Application Settings...";
            try
            {
                string TbarId = Context.QueryString["TbarId"].ToString();

                msg = $"Starting RDP For Tbar: {TbarId}";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);

                msg = $"Killing any previous RDPs";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                KillRDPSession();

                msg = $"Killed All Previous RDP For Tbar: {TbarId}";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                TimeBoundAccessRequest tbar = SimpleDatabaseHandler<TimeBoundAccessRequest>.QueryWithStoredProc("GetTbarById2", TbarId)[0];
                CoreAppXmlConfiguration.Instance.BusyTimeout = 20000;

                //send message
                msg = $"Launching new TV Session for Tbar: {TbarId}";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                Application app = TestStack.White.Application.Launch(@"E:\Software\TeamViewer_Setup.exe");

                //send message
                msg = $"TV program has been started Current State:Initializing";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                TestStack.White.UIItems.WindowItems.Window tvWindow = GetTeamViewerWindow(app);

                msg = $"TV program has been started Current State:Clicking Accept Button";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                ApiResult btnClickResult = SelectDefaultSettings(tvWindow);

                if (btnClickResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    msg = $"Failed to click button. Reason: {btnClickResult.StatusDesc}. Retrying..";
                    // call client-side SendMethod method
                    Clients.Caller.sendMessage(msg);
                    Thread.Sleep(new TimeSpan(0, 0, 3));
                    StartRDPSession(clients);
                    return;
                }

                msg = $"TV program has been started Current State:waiting for TV to re-Start";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                tvWindow = GetTeamViewerWindow(app);

                //send message
                msg = $"TV program has been started Current State:Connecting";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);

                //give tv some seconds to intialize
                ApiResult readyResult = ConfirmTVReadinessForConnection(tvWindow);
                if (btnClickResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    msg = $"Failed to confirm TV readiness: {readyResult.StatusDesc}. Retrying...";
                    // call client-side SendMethod method
                    Clients.Caller.sendMessage(msg);
                    Thread.Sleep(new TimeSpan(0, 0, 3));
                    StartRDPSession(clients);
                    return;
                }

                //send message
                msg = $"TV program has been started Current State:Taking Screen Shot";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                //Takes a screenshot of the entire desktop, and saves it to disk
                ApiResult screenShotResult = TakeDesktopScreenShot(tvWindow, Context);

                if (btnClickResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    msg = $"Failed to Take Screen Shot. Reason {screenShotResult.StatusDesc}. Retrying...";
                    // call client-side SendMethod method
                    Clients.Caller.sendMessage(msg);
                    Thread.Sleep(new TimeSpan(0, 0, 3));
                    StartRDPSession(clients);
                    return;
                }

                //send message
                msg = $"TV program has been started Current State:Ready to Connect";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);

                //send message
                msg = $"Done.ImageURL: Images/{screenShotResult.PegPayID}";
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
            }
            catch (Exception ex)
            {
                msg = "ERROR: " + ex.Message + " " + ex.StackTrace;
                // call client-side SendMethod method
                Clients.Caller.sendMessage(msg);
                Thread.Sleep(new TimeSpan(0, 0, 3));
                StartRDPSession(clients);
                return;
            }
        }

        private static ApiResult TakeDesktopScreenShot(Window tvWindow, HubCallerContext Context)
        {
            ApiResult result = new ApiResult();
            try
            {
                GetWindowFocus(tvWindow);
                Bitmap bitmap = Desktop.CaptureScreenshot();
                string ImageName = DateTime.Now.Ticks.ToString() + ".jpg";
                string ImagesFolder = Context.Request.GetHttpContext().Server.MapPath("Images/").Replace(@"signalr\", string.Empty);
                String saveImagePath = ImagesFolder + ImageName;
                bitmap.Save(saveImagePath);
                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                result.PegPayID = ImageName;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "ERROR: " + ex.Message;
            }
            return result;
        }

        private static ApiResult GetWindowFocus(Window tvWindow)
        {
            ApiResult result = new ApiResult();
            try
            {
                tvWindow.Focus(DisplayState.Maximized);
                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "ERROR: " + ex.Message;
                //return GetWindowFocus(tvWindow);
            }
            return result;
        }



        private static ApiResult SelectDefaultSettings(Window tvWindow)
        {
            ApiResult result = new ApiResult();
            try
            {
                TestStack.White.UIItems.RadioButton runOnceCheckBox = tvWindow?.Items.FirstOrDefault(w => w.Name.ToUpper().Contains("ONE TIME")) as TestStack.White.UIItems.RadioButton;
                GetWindowFocus(tvWindow);
                runOnceCheckBox.Select();
                GetWindowFocus(tvWindow);
                TestStack.White.UIItems.Button button = tvWindow?.Items.FirstOrDefault(w => w.Name.ToUpper().Contains("ACCEPT")) as TestStack.White.UIItems.Button;
                GetWindowFocus(tvWindow);
                button.Click();
                GetWindowFocus(tvWindow);
                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "ERROR: " + ex.Message;
            }
            return result;
        }

        private static ApiResult KillRDPSession()
        {
            ApiResult result = new ApiResult();
            try
            {
                bool haveFoundAWindow = true;

                while (haveFoundAWindow)
                {
                    haveFoundAWindow = Kill();
                    Thread.Sleep(new TimeSpan(0, 0, 10));
                }

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "ERROR: " + ex.Message;
            }
            return result;
        }

        private static bool Kill()
        {
            bool haveFoundAWindow = false;
            Desktop.Instance.Windows().ForEach(i =>
            {
                if (i.Title.ToUpper().Contains("TEAMVIEWER")|| i.Title.ToUpper().Contains("TVUPDATE"))
                {
                    i.Close();
                    haveFoundAWindow = true;
                }
            });
            return haveFoundAWindow;
        }

        private static ApiResult ConfirmTVReadinessForConnection(Window tvWindow)
        {
            ApiResult result = new ApiResult();
            try
            {
                Label label = tvWindow.Items.FirstOrDefault(i => i.Name.ToUpper().Contains("READY TO CONNECT")) as Label;
                if (label == null)
                {
                    Thread.Sleep(new TimeSpan(0, 0, 10));
                    return ConfirmTVReadinessForConnection(tvWindow);
                }
                Thread.Sleep(new TimeSpan(0, 0, 5));
                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "ERROR: " + ex.Message;
            }
            return result;
        }

        private static TestStack.White.UIItems.WindowItems.Window GetTeamViewerWindow(Application app)
        {
            try
            {
                TestStack.White.UIItems.WindowItems.Window window = Desktop.Instance.Windows().FirstOrDefault(w => w.Title.ToUpper().Contains("TEAMVIEWER"));
                if (window == null)
                {
                    Thread.Sleep(new TimeSpan(0, 0, 5));
                    return GetTeamViewerWindow(app);
                }
                return window;
            }
            catch (Exception ex)
            {
                return GetTeamViewerWindow(app);
            }
        }


    }
}