using ChangeRequestSubSystem.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.ControlClasses
{
    public class NotificationsHandler
    {
        public static ApiResult SendApproveCrEmail(ApproverForChangeRequest link)
        {
            ApiResult result = new ApiResult();
            try
            {
                SystemUser approver = SystemUser.QueryWithStoredProc("GetSystemUserById", link.ApproverId).FirstOrDefault();

                if (approver == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"USER WITH THE APPROVER ID {link.ApproverId} WAS NOT FOUND";
                    return result;
                }

                ChangeRequest changeRequest = ChangeRequest.QueryWithStoredProc("GetChangeRequestById", link.ChangeRequestId).FirstOrDefault();

                if (changeRequest == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"CHANGE REQUEST WITH THE ID {link.ChangeRequestId} WAS NOT FOUND";
                    return result;
                }

                string filePath = SystemSetting.QueryWithStoredProc("GetSystemSettingById", Globals.FILE_PATH_TO_APPROVE_CR_EMAIL_TEMPLATE).FirstOrDefault()?.SettingValue;

                if (filePath == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"{nameof(Globals.FILE_PATH_TO_APPROVE_CR_EMAIL_TEMPLATE)} NOT FOUND";
                    return result;
                }

                string ApproveURL = SystemSetting.QueryWithStoredProc("GetSystemSettingById", Globals.APPROVE_CR_URL).FirstOrDefault()?.SettingValue;

                if (ApproveURL == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"{nameof(Globals.APPROVE_CR_URL)} NOT FOUND";
                    return result;
                }

                string msg = File.ReadAllText(filePath);
                string dateFormat = "yyyy-MM-dd HH:mm";


                msg = msg.Replace("[APPROVER_NAME]", approver.Username);
                msg = msg.Replace("[TITLE]", changeRequest.Title);
                msg = msg.Replace("[CURRENT_PROBLEM]", changeRequest.Problem);
                msg = msg.Replace("[PROPOSED_SOLUTION]", changeRequest.Solution);
                msg = msg.Replace("[CHANGE_CATEGORY]", changeRequest.ChangeCategoryId);
                msg = msg.Replace("[JUSTIFICATION]", changeRequest.Justification);
                msg = msg.Replace("[IMPACT]", changeRequest.ImpactOfNotImplementing);
                msg = msg.Replace("[REQUESTOR_NAME]", changeRequest.RequesterName);
                msg = msg.Replace("[REQUESTOR_EMAIL]", changeRequest.RequesterEmail);
                msg = msg.Replace("[REQUESTOR_PHONE]", changeRequest.RequesterPhone);
                msg = msg.Replace("[REQUESTOR_COMPANY]", changeRequest.RequesterCompany);
                msg = msg.Replace("[IMPLEMENTER_NAME]", changeRequest.ImplementerName);
                msg = msg.Replace("[IMPLEMENTER_EMAIL]", changeRequest.ImplementerEmail);
                msg = msg.Replace("[IMPLEMENTER_PHONE]", changeRequest.ImplementerPhone);
                msg = msg.Replace("[START_DATE]", changeRequest.ChangeStartDateTime.ToString(dateFormat));
                msg = msg.Replace("[END_DATE]", changeRequest.ChangeEndDateTime.ToString(dateFormat));
                msg = msg.Replace("[APPROVE_URL]", ApproveURL);
                msg = msg.Replace("[USER_ID]", link.ApproverId);
                msg = msg.Replace("[CR_ID]", changeRequest.ChangeRequestId);

                MailMessage mail = new MailMessage(Globals.FROM_EMAIL, approver.Email);
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = Globals.SMPTP_SERVER;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(Globals.SMTP_USERNAME, Globals.SMTP_PASSWORD);
                mail.Subject = Globals.MAIL_SUBJECT_APPROVE_CR_EMAIL;
                mail.Body = msg;
                mail.IsBodyHtml = true;
                client.Send(mail);

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "EXCEPTION: " + ex.Message;
                return result;
            }
            return result;
        }

        public static ApiResult SendApproveTbarEmail(TimeBoundAccessRequest tbar)
        {
            ApiResult result = new ApiResult();
            try
            {
                SystemUser approver = SystemUser.QueryWithStoredProc("GetSystemUserById", tbar.Approver).FirstOrDefault();

                if (approver == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"USER WITH THE APPROVER ID {tbar.Approver} WAS NOT FOUND";
                    return result;
                }



                string filePath = SystemSetting.QueryWithStoredProc("GetSystemSettingById", Globals.FILE_PATH_TO_APPROVE_TBAR_EMAIL_TEMPLATE).FirstOrDefault()?.SettingValue;

                if (filePath == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"{nameof(Globals.FILE_PATH_TO_APPROVE_TBAR_EMAIL_TEMPLATE)} NOT FOUND";
                    return result;
                }

                string ApproveURL = SystemSetting.QueryWithStoredProc("GetSystemSettingById", Globals.APPROVE_TBAR_URL).FirstOrDefault()?.SettingValue;

                if (ApproveURL == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"{nameof(Globals.APPROVE_TBAR_URL)} NOT FOUND";
                    return result;
                }

                string msg = File.ReadAllText(filePath);
                string dateFormat = "yyyy-MM-dd HH:mm";


                msg = msg.Replace("[APPROVER_NAME]", approver.Username);
                msg = msg.Replace("[REQUESTOR_NAME]", tbar.UserId);
                msg = msg.Replace("[SYSTEM_NAME]", tbar.SystemCode);
                msg = msg.Replace("[TYPE_OF_ACCESS]", tbar.TypeOfAccess);
                msg = msg.Replace("[START_DATE]", tbar.StartTime.ToString(dateFormat));
                msg = msg.Replace("[DURATION]", tbar.DurationInMinutes.ToString());
                msg = msg.Replace("[REASON]", tbar.Reason);
                msg = msg.Replace("[APPROVE_URL]", ApproveURL);
                msg = msg.Replace("[USER_ID]", approver.Username);
                msg = msg.Replace("[TBAR_ID]", tbar.TBPAccessId);

                MailMessage mail = new MailMessage(Globals.FROM_EMAIL, approver.Email);
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = Globals.SMPTP_SERVER;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(Globals.SMTP_USERNAME, Globals.SMTP_PASSWORD);
                mail.Subject = Globals.MAIL_SUBJECT_APPROVE_TBAR_EMAIL;
                mail.Body = msg;
                mail.IsBodyHtml = true;
                client.Send(mail);

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result.StatusCode = Globals.FAILURE_STATUS_CODE;
                result.StatusDesc = "EXCEPTION: " + ex.Message;
                return result;
            }
            return result;
        }

        public static ApiResult SendCrRejectedEmail(ChangeRequest cr, string reason)
        {
            ApiResult result = new ApiResult();
            try
            {
                MailApi.Email mail = new MailApi.Email();
                MailApi.EmailAddress addr = new MailApi.EmailAddress();
                addr.Address = cr.RequesterEmail;
                addr.Name = cr.RequesterName;
                addr.AddressType = MailApi.EmailAddressType.To;

                MailApi.EmailAddress[] addresses = { addr };
                mail.MailAddresses = addresses;
                mail.From = Globals.MAIL_FROM;
                mail.Message = $"Hi,\n<br/>" +
                               $"Your Change Request has been REJECTED\n<br/>\n<br/>" +
                               $"ID:  {cr.ChangeRequestId}\n<br/>" +
                               $"Title:  {cr.Title}\n<br/>" +
                               $"Current Problem:  {cr.Problem}\n<br/>" +
                               $"Suggested Solution  {cr.Solution}\n<br/>" +
                               $"Date Of CR:  {cr.CreatedOn}\n<br/>" +
                               $"Reason For Rejection:  {reason}\n<br/>" +
                               $"You can reapply for another CR with the necessary Changes\n<br/>" +
                               $"\n<br/>Thank you.\n<br/>" +
                               $"Pegasus Change Request System.\n<br/>" +
                               $"#DontHateTheMessengerOfBadNews\n<br/>";

                mail.Subject = "CHANGE REQUEST REJECTED";

                MailApi.MessengerSoapClient mapi = new MailApi.MessengerSoapClient();

                MailApi.Result resp = mapi.PostEmail(mail);

                if (resp.StatusCode != "0")
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = resp.StatusDesc;
                    return result;
                }

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendCrRejectedEmail), $"{cr.ChangeRequestId}", ex);
            }
            return result;
        }

        public static ApiResult SendCrApprovedEmail(ChangeRequest cr)
        {
            ApiResult result = new ApiResult();
            try
            {
                MailApi.Email mail = new MailApi.Email();
                MailApi.EmailAddress addr = new MailApi.EmailAddress();
                addr.Address = cr.RequesterEmail;
                addr.Name = cr.RequesterName;
                addr.AddressType = MailApi.EmailAddressType.To;

                MailApi.EmailAddress[] addresses = { addr };
                mail.MailAddresses = addresses;
                mail.From = Globals.MAIL_FROM;
                mail.Message = $"Hi,\n<br/>" +
                               $"Congrajulations. Your Change Request has been APPROVED!!\n<br/>\n<br/>" +
                               $"ID:  {cr.ChangeRequestId}\n<br/>" +
                               $"Title:  {cr.Title}\n<br/>" +
                               $"Current Problem:  {cr.Problem}\n<br/>" +
                               $"Suggested Solution  {cr.Solution}\n<br/>" +
                               $"Date Of CR:  {cr.CreatedOn}\n<br/>" +
                               $"You can also apply for a T.B.A.R inorder to implement Changes\n<br/>" +
                               $"\n<br/>Thank you.\n<br/>" +
                               $"Pegasus Change Request System.\n<br/>" +
                               $"#SuccessIsSweet\n<br/>";

                mail.Subject = "CHANGE REQUEST APPROVED";

                MailApi.MessengerSoapClient mapi = new MailApi.MessengerSoapClient();

                MailApi.Result resp = mapi.PostEmail(mail);

                if (resp.StatusCode != "0")
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = resp.StatusDesc;
                    return result;
                }

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendCrApprovedEmail), $"{cr.ChangeRequestId}", ex);
            }
            return result;
        }

        internal static ApiResult SendOneTimePINByEmail(string preferedContact, string OTP)
        {
            ApiResult result = new ApiResult();
            try
            {
                //send the OTP
                MailApi.Email mail = new MailApi.Email();
                MailApi.EmailAddress addr = new MailApi.EmailAddress();
                addr.Address = preferedContact;
                addr.Name = preferedContact;
                addr.AddressType = MailApi.EmailAddressType.To;

                MailApi.EmailAddress[] addresses = { addr };
                mail.MailAddresses = addresses;
                mail.From = Globals.MAIL_FROM;
                mail.Message = $"Hi<br/>,Your One Time PIN For the CR System is {OTP}";
                mail.Subject = Globals.MAIL_SUBJECT;

                MailApi.MessengerSoapClient mapi = new MailApi.MessengerSoapClient();

                Task.Factory.StartNew(() =>
                {
                    MailApi.Result resp = mapi.PostEmail(mail);
                });
               

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                result.PegPayID = preferedContact;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendOneTimePINByPhone), $"{preferedContact}", ex);
            }
            return result;
        }

        internal static ApiResult HandleException(string methodName, string message, Exception ex)
        {
            ApiResult apiResult = new ApiResult();
            Task.Factory.StartNew(() =>
            {
                DatabaseHandler dh = new DatabaseHandler();
                dh.LogError(methodName, message);
            });
            apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
            apiResult.StatusDesc = $"EXCEPTION: {ex.Message}";
            return apiResult;
        }

        public static ApiResult SendOneTimePINByPhone(string preferedContact, string OTP)
        {
            ApiResult result = new ApiResult();
            try
            {
                //send the OTP
                MailApi.SMS sms = new MailApi.SMS();
                sms.Mask = Globals.SMS_MASK;
                sms.Sender = Globals.SMS_SENDER;
                sms.Message = $"Hi, Your One Time PIN For CR System is {OTP}";
                sms.Phone = preferedContact;
                sms.Reference = DateTime.Now.Ticks.ToString();
                sms.VendorTranId = DateTime.Now.Ticks.ToString();

                MailApi.MessengerSoapClient mailApi = new MailApi.MessengerSoapClient();

                Task.Factory.StartNew(() =>
                {
                    MailApi.Result resp = mailApi.SendSMS(sms);
                });
                //MailApi.Result resp = mailApi.SendSMS(sms);

                //if (resp.StatusCode != "0")
                //{
                //    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                //    result.StatusDesc = resp.StatusDesc;
                //    return result;
                //}

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                result.PegPayID = preferedContact;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendOneTimePINByPhone), $"{preferedContact}", ex);
            }
            return result;
        }

        internal static ApiResult SendTbarRejectedEmail(TimeBoundAccessRequest tbar, string Reason)
        {
            ApiResult result = new ApiResult();
            try
            {
                SystemUser user = SystemUser.QueryWithStoredProc("GetSystemUserById", tbar.UserId).FirstOrDefault();

                if (user == null)
                {
                    result.SetFailuresAsStatusInResponseFields($"USER WITH ID {tbar.UserId} NOT FOUND");
                    return result;
                }

                MailApi.Email mail = new MailApi.Email();
                MailApi.EmailAddress addr = new MailApi.EmailAddress();
                addr.Address = user.Email;
                addr.Name = tbar.UserId;
                addr.AddressType = MailApi.EmailAddressType.To;

                MailApi.EmailAddress[] addresses = { addr };
                mail.MailAddresses = addresses;
                mail.From = Globals.MAIL_FROM;
                mail.Message = $"Hi,\n<br/>" +
                               $"Your Change Request has been REJECTED\n<br/>\n<br/>" +
                               $"ID:  {tbar.TBPAccessId}\n<br/>" +
                               $"System Name:  {tbar.SystemCode}\n<br/>" +
                               $"Duration In Minutes:  {tbar.DurationInMinutes}\n<br/>" +
                               $"Reason:  {tbar.Reason}\n<br/>" +
                               $"Date Of CR:  {tbar.CreatedOn}\n<br/>" +
                               $"Reason For Rejection:  {Reason}\n<br/>" +
                               $"You can reapply for another Tbar with the necessary Changes\n<br/>" +
                               $"\n<br/>Thank you.\n<br/>" +
                               $"Pegasus Change Request System.\n<br/>" +
                               $"#DontHateTheMessenger\n<br/>";

                mail.Subject = "Time Bound Access Request REJECTED";

                MailApi.MessengerSoapClient mapi = new MailApi.MessengerSoapClient();

                MailApi.Result resp = mapi.PostEmail(mail);

                if (resp.StatusCode != "0")
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = resp.StatusDesc;
                    return result;
                }

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendCrRejectedEmail), $"{tbar.TBPAccessId}", ex);
            }
            return result;
        }

        internal static ApiResult SendTbarApprovedEmail(TimeBoundAccessRequest tbar)
        {
            ApiResult result = new ApiResult();
            try
            {
                SystemUser user = SystemUser.QueryWithStoredProc("GetSystemUserById", tbar.UserId).FirstOrDefault();

                if (user == null)
                {
                    result.SetFailuresAsStatusInResponseFields($"USER WITH ID {tbar.UserId} NOT FOUND");
                    return result;
                }

                MailApi.Email mail = new MailApi.Email();
                MailApi.EmailAddress addr = new MailApi.EmailAddress();
                addr.Address = user.Email;
                addr.Name = tbar.UserId;
                addr.AddressType = MailApi.EmailAddressType.To;

                MailApi.EmailAddress[] addresses = { addr };
                mail.MailAddresses = addresses;
                mail.From = Globals.MAIL_FROM;
                string dateFormat = Globals.DATE_FORMAT;
                mail.Message = $"Hi,\n<br/>" +
                               $"Congrajulations. Your Change Request has been APPROVED!!\n<br/>\n<br/>" +
                               $"ID:  {tbar.TBPAccessId}\n<br/>" +
                               $"System Name:  {tbar.SystemCode}\n<br/>" +
                               $"Duration In Minutes:  {tbar.DurationInMinutes}\n<br/>" +
                               $"Reason:  {tbar.Reason}\n<br/>" +
                               $"Date Of CR:  {tbar.CreatedOn}\n<br/>" +
                               $"Reason :  {tbar.Reason}\n<br/>" +
                               $"You can login at  {tbar.StartTime.ToString(dateFormat)} and Use Tbar\n<br/>" +
                               $"\n<br/>Thank you.\n<br/>" +
                               $"Pegasus Change Request System.\n<br/>";

                mail.Subject = "Time Bound Access Request APPROVED";

                MailApi.MessengerSoapClient mapi = new MailApi.MessengerSoapClient();

                MailApi.Result resp = mapi.PostEmail(mail);

                if (resp.StatusCode != "0")
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = resp.StatusDesc;
                    return result;
                }

                result.StatusCode = Globals.SUCCESS_STATUS_CODE;
                result.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendCrRejectedEmail), $"{tbar.TBPAccessId}", ex);
            }
            return result;
        }
    }
}
