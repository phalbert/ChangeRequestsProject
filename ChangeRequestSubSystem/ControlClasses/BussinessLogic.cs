using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using ChangeRequestSubSystem.Entities;

namespace ChangeRequestSubSystem.ControlClasses
{
    public class BussinessLogic
    {
        private List<string> AcceptableMethodsOfSendingOTP = new List<string>() { "EMAIL", "PHONE" };

        public BussinessLogic()
        {

        }

        internal ApiResult Initialize()
        {
            ApiResult apiResult = new ApiResult();

            try
            {
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;

                List<Type> typesToKeepTrackOf = GetTypesToKeepTrackOf();

                ActiveRecordStarter.Initialize(source, typesToKeepTrackOf.ToArray());
                ActiveRecordStarter.UpdateSchema();
                Seed();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError("OnInitialize", "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields("FAILED TO INTIALIZE API");
            }

            return apiResult;
        }

        internal ApiResult DropAndRecreate()
        {
            ApiResult apiResult = new ApiResult();

            try
            {
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
                List<Type> typesToKeepTrackOf = GetTypesToKeepTrackOf();
                ActiveRecordStarter.Initialize(source, typesToKeepTrackOf.ToArray());
                ActiveRecordStarter.DropSchema();
                ActiveRecordStarter.UpdateSchema();
                Seed();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError("OnInitialize", "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields("FAILED TO INTIALIZE API");
            }

            return apiResult;
        }

        private static List<Type> GetTypesToKeepTrackOf()
        {
            return new List<Type>
                {
                    typeof(SystemAffected),
                    typeof(ApiLog),
                    typeof(PostChangeTest),
                    typeof(ApproverToChangeRequestLink),
                    typeof(Company),
                    typeof(SystemUser),
                    typeof(RiskAnalysis),
                    typeof(CR_Attachment),
                    typeof(Role),
                    typeof(ServicesAffected),
                    typeof(ChangeRequest),
                    typeof(OneTimePassword),
                    typeof(RollBackPlan),
                    typeof(TimeBoundAccessRequest)
                };
        }

        private bool HandleError(string Id, string type, string message)
        {
            try
            {
                //try to log errror
                Task.Factory.StartNew(() =>
                {
                    ApiLog log = new ApiLog();
                    log.LogInfo(Id, type, message);
                });

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        internal ApiResult AttachSystemAffectedToChangeRequest(SystemAffected sys)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!sys.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(sys.StatusDesc);
                    return apiResult;
                }

                sys.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AttachPostChangeTestsToChangeRequest(PostChangeTest postChangeTest)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!postChangeTest.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(postChangeTest.StatusDesc);
                    return apiResult;
                }

                postChangeTest.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AttachRiskAnalysisToChangeRequest(RiskAnalysis riskAnalysis)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!riskAnalysis.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(riskAnalysis.StatusDesc);
                    return apiResult;
                }

                riskAnalysis.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult SaveTimeBoundAccessRequest(TimeBoundAccessRequest req)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!req.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(req.StatusDesc);
                    return apiResult;
                }

                req.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult SaveSystemUser(SystemUser systemUser)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!systemUser.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(systemUser.StatusDesc);
                    return apiResult;
                }

                SystemUser old = SystemUser.QueryWithStoredProc("GetSystemUserByID", systemUser.Username).FirstOrDefault();

                systemUser.Id = old != null ? old.Id : systemUser.Id;

                systemUser.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult Seed()
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                SystemUser user = new SystemUser();
                user.CompanyCode = "PEGASUS";
                user.Email = "kasozi.nsubuga@pegasus.co.ug";
                user.Username = "nsubugak";
                user.RoleCode = "SUPER-ADMIN";
                user.ModifiedBy = "Admin";
                user.PhoneNumber = "256752001311";

                SaveSystemUser(user);

            }
            catch (Exception ex)
            {
                apiResult = HandleException(nameof(SendOneTimePIN), $"SeedError, Error:{ex.Message}", ex);
            }
            return apiResult;
        }

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

                SystemUser[] systemUsers = SystemUser.QueryWithStoredProc("GetSystemUserByID", username);

                if (systemUsers.Count() <= 0)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = $"User with Username [{username}] doesnt exist";
                    return apiResult;
                }

                SystemUser user = systemUsers[0];

                OneTimePassword oneTimePassword = new OneTimePassword();
                oneTimePassword.CompanyCode = user.CompanyCode;
                oneTimePassword.Password = "1234";
                oneTimePassword.ValidityDurationInSeconds = 5 * 60;
                oneTimePassword.Username = user.Username;
                oneTimePassword.Save();


                ApiResult sendResult = MethodOfSending.ToUpper() == "PHONE" ? SendOneTimePINByPhone(user.PhoneNumber, oneTimePassword.Password) : SendOneTimePINByEmail(user.Email, oneTimePassword.Password);


                if (sendResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    apiResult.StatusCode = Globals.FAILURE_STATUS_CODE;
                    apiResult.StatusDesc = "Send One Time PIN failed: " + sendResult.StatusDesc;
                    return apiResult;
                }

                apiResult.StatusCode = Globals.SUCCESS_STATUS_CODE;
                apiResult.StatusDesc = $"Successfully Sent One time Password by {MethodOfSending} to {sendResult.PegPayID}. Its Valid for {oneTimePassword.ValidityDurationInSeconds/60} minute(s)";
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
                return DatabaseHandler.ExecuteDataSet(storedProc, parameters);
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

        internal DataSet ExecuteSqlQuery(string SqlQuery)
        {
            try
            {
                return DatabaseHandler.ExecuteSqlQuery(SqlQuery);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    DatabaseHandler dh = new DatabaseHandler();
                    dh.LogError(nameof(SendOneTimePIN), $"{SqlQuery}, Error:{ex.Message}");
                });
                throw ex;
            }
        }

        internal int ExecuteNonQuery(string SqlQuery)
        {
            try
            {
                return DatabaseHandler.ExecuteNonQuery(SqlQuery);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    DatabaseHandler dh = new DatabaseHandler();
                    dh.LogError(nameof(SendOneTimePIN), $"{SqlQuery}, Error:{ex.Message}");
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
                result.PegPayID = preferedContact;
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

        internal ApiResult UpdateChangeRequestStatus(ApproverToChangeRequestLink link)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!link.IsValidUpdate())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(link.StatusDesc);
                    return apiResult;
                }

                ApproverToChangeRequestLink old = ApproverToChangeRequestLink.QueryWithStoredProc("GetApproverToChangeRequest", link.UserId, link.ChangeRequestId).FirstOrDefault();

                if (old == null)
                {
                    link.Save();
                }
                else
                {
                    old.Decision = link.Decision;
                    old.Reason = link.Reason;
                    old.Save();
                }

                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(UpdateChangeRequestStatus), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AssignApproverToChangeRequest(ApproverToChangeRequestLink link)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!link.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(link.StatusDesc);
                    return apiResult;
                }


                link.Save();

                apiResult.PegPayID = link.Id.ToString();
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(AssignApproverToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
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
                    apiResult.SetFailuresAsStatusInResponseFields(req.StatusDesc);
                    return apiResult;
                }

                req.Save();

                apiResult.PegPayID = "" + req.Id;
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(SaveRole), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
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
                    apiResult.SetFailuresAsStatusInResponseFields(req.StatusDesc);
                    return apiResult;
                }

                req.Save();

                apiResult.PegPayID = "" + req.Id;
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(SaveCompany), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AttachRollBackPlanToChangeRequest(RollBackPlan req)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!req.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(req.StatusDesc);
                    return apiResult;
                }

                req.Save();

                apiResult.PegPayID = "" + req.Id;
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(SaveCompany), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AttachItemToChangeRequest(CR_Attachment req)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                req.Hash = GenerateSHA256String(req.Base64StringOfContent);

                if (!req.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(req.StatusDesc);
                    return apiResult;
                }

                req.Save();

                apiResult.PegPayID = "" + req.Id;
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(SaveCompany), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        internal ApiResult SaveChangeRequest(ChangeRequest changeRequest)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!changeRequest.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(changeRequest.StatusDesc);
                    return apiResult;
                }

                changeRequest.Save();

                apiResult.PegPayID = "" + changeRequest.Id;
                apiResult.SetSuccessAsStatusInResponseFields();
                return apiResult;
            }
            catch (Exception ex)
            {
                HandleError(nameof(SaveCompany), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
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

                SystemUser[] systemUsers = SystemUser.QueryWithStoredProc("GetSystemUserByID", username);

                if (systemUsers.Count() <= 0)
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Invalid username supplied";
                    return user;
                }

                user = systemUsers[0];
                //IEnumerable
                OneTimePassword[] oneTimePasswords = OneTimePassword.QueryWithStoredProc("GetLatestOTP", username);

                if (oneTimePasswords.Count() <= 0)
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"No OTP found for {username}";
                    return user;
                }

                OneTimePassword oneTimePassword = oneTimePasswords[0];


                if (oneTimePassword.Password != password)
                {
                    user.StatusCode = Globals.FAILURE_STATUS_CODE;
                    user.StatusDesc = $"Invalid Password Supplied";
                    return user;
                }

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
                result.PegPayID = preferedContact;
            }
            catch (Exception ex)
            {
                result = HandleException(nameof(SendOneTimePINByPhone), $"{preferedContact}", ex);
            }
            return result;
        }
    }
}
