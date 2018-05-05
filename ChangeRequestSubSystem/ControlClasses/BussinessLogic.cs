using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeRequestSubSystem.Entities;

namespace ChangeRequestSubSystem.ControlClasses
{
    public class BussinessLogic
    {
        private List<string> AcceptableMethodsOfSendingOTP = new List<string>() { "EMAIL", "PHONE" };

        internal ApiResult SendOneTimePIN(string username, string MethodOfSending)
        {
            ApiResult apiResult = new ApiResult();
            try
            {

                if (string.IsNullOrEmpty(username))
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"Please Supply a Username";
                    return apiResult;
                }
                if (!AcceptableMethodsOfSendingOTP.Contains(MethodOfSending.ToUpper()))
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"Please Specify how you want to recieve the OTP";
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.GetUserByID(username);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"User with Username [{username}] doesnt exist";
                    return apiResult;
                }


                string Phone = dt.Rows[0]["PhoneNumber"].ToString();
                string Email = dt.Rows[0]["Email"].ToString();
                string preferedContact = MethodOfSending.ToUpper() == "PHONE" ? Phone : Email;
                string OTP = "1234"; //GenerateOTP(preferedContact);

                dt = dh.SaveOneTimePIN(username, OTP);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"FAILED TO LOG OTP";
                    return apiResult;
                }

                ApiResult sendResult = MethodOfSending.ToUpper() == "PHONE" ? SendOneTimePINByPhone(preferedContact, OTP) : SendOneTimePINByEmail(preferedContact, OTP);


                if (sendResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = "Send One Time PIN failed: " + sendResult.StatusDesc;
                    return apiResult;
                }

                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(SendOneTimePIN), $"{username}, Error:{ex.Message}", ex);
            }
            return apiResult;
        }

        internal DataSet ExecuteDataSet(string storedProc, object[] parameters)
        {
            try
            {
                DatabaseHandler dh = new DatabaseHandler();
                return dh.ExecuteDataSet(storedProc, parameters);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    DatabaseHandler dh = new DatabaseHandler();
                    dh.LogError(nameof(SendOneTimePIN), $"{storedProc}, Error:{ex.Message}");
                });
                throw ex;
            }
        }

        private ApiResult SendOneTimePINByEmail(string preferedContact, string OTP)
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

                MailApi.Result resp = mapi.PostEmail(mail);

                if (resp.StatusCode != "0")
                {
                    Console.WriteLine("Failed to Send Email: " + resp.StatusDesc);
                    throw new Exception("Failed to Send Mail: " + resp.StatusDesc);
                }


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
                result = HandleException(nameof(SendOneTimePINByPhone), $"{preferedContact}", ex);
            }
            return result;
        }

        private ApiResult HandleException(string methodName, string message, Exception ex)
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

        internal ApiResult UpdateChangeRequestStatus(Approver approver, ChangeRequest changeRequest, string Decision)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!changeRequest.IsValidUpdateRequest())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = changeRequest.StatusDesc;
                    return apiResult;
                }

                if (!approver.IsValidUpdateRequest())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = approver.StatusDesc;
                    return apiResult;
                }

                if (string.IsNullOrEmpty(Decision))
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = "Please Supply a Decision";
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.UpdateChangeRequestStatus(changeRequest, approver, Decision);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"NO ROWS AFFECTED";
                    return apiResult;
                }

                string InsertedID = dt.Rows[0][0].ToString();

              
                if (InsertedID != "WAIT")
                {
                    //TODO: Send out Success or Failure Notification
                }

                apiResult.PegPayID = InsertedID;
                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(UpdateChangeRequestStatus), $"{changeRequest.ChangeRequestId}:{approver.Username}", ex);
            }
            return apiResult;
        }

        internal ApiResult AssignChangeRequestToApprover(ChangeRequest changeRequest, Approver approver)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!changeRequest.IsValidAssignRequest())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = changeRequest.StatusDesc;
                    return apiResult;
                }

                if (!approver.IsValidAssignRequest())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = approver.StatusDesc;
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.AssignChangeRequestToApprover(changeRequest, approver);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"NO ROWS AFFECTED";
                    return apiResult;
                }

                string InsertedID = dt.Rows[0][0].ToString();
                apiResult.PegPayID = InsertedID;
                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(AssignChangeRequestToApprover), $"{changeRequest.ChangeRequestId}:{approver.Username}", ex);
            }
            return apiResult;
        }

        internal ApiResult SaveRole(Role req)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!req.IsValid())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = req.StatusDesc;
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.SaveRole(req);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"NO ROWS AFFECTED";
                    return apiResult;
                }

                string InsertedID = dt.Rows[0][0].ToString();
                apiResult.PegPayID = InsertedID;
                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(SaveRole), $"{req.RoleCode}", ex);
            }
            return apiResult;
        }

        internal ApiResult SaveCompany(Company req)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!req.IsValid())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = req.StatusDesc;
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.SaveCompany(req);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"NO ROWS AFFECTED";
                    return apiResult;
                }

                string InsertedID = dt.Rows[0][0].ToString();
                apiResult.PegPayID = InsertedID;
                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(SaveCompany), $"{req.CompanyCode}", ex);
            }
            return apiResult;
        }

        internal ApiResult SaveChangeRequest(ChangeRequest changeRequest)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!changeRequest.IsValid())
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = changeRequest.StatusDesc;
                    return apiResult;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataTable dt = dh.SaveChangeRequest(changeRequest);

                if (dt.Rows.Count < 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"NO ROWS AFFECTED";
                    return apiResult;
                }

                string InsertedID = dt.Rows[0][0].ToString();
                apiResult.PegPayID = InsertedID;
                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(SaveCompany), $"{changeRequest.ChangeRequestId}", ex);
            }
            return apiResult;
        }

        internal SystemUser Login(string username, string password)
        {
            SystemUser user = new SystemUser();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Please Supply a Username";
                    return user;
                }

                if (string.IsNullOrEmpty(password))
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Please Supply a password";
                    return user;
                }

                DatabaseHandler dh = new DatabaseHandler();
                DataSet ds = dh.CheckUserCreds(username, password);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count < 0)
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Invalid Username or Password";
                    return user;
                }

                string generatedOTP = dt.Rows[0]["GeneratedOTP"].ToString();

                if (generatedOTP != password)
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Invalid Username or Password";
                    return user;
                }

                dt = ds.Tables[1];
                user.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
                user.CompanyEmail = dt.Rows[0]["Email"].ToString();
                user.CompanyCode = dt.Rows[0]["CompanyCode"].ToString();

                user.StatusCode = Globals.SUCCESS_STATUS_CODE;
                user.StatusDesc = Globals.SUCCESS_STATUS_TEXT;
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    DatabaseHandler dh = new DatabaseHandler();
                    dh.LogError(nameof(SendOneTimePIN), $"{username}, Error:{ex.Message}");
                });
                user.StatusCode = Globals.FAILURE_STATUS_CODE;
                user.StatusDesc = $"EXCEPTION: {ex.Message}";
            }
            return user;
        }

        private ApiResult SendOneTimePINByPhone(string preferedContact, string OTP)
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
                MailApi.Result resp = mailApi.SendSMS(sms);

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
                result = HandleException(nameof(SendOneTimePINByPhone), $"{preferedContact}", ex);
            }
            return result;
        }
    }
}
