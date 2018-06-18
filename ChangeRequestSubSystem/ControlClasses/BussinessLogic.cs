using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using ChangeRequestSubSystem.Entities;

namespace ChangeRequestSubSystem.ControlClasses
{
    public class BussinessLogic
    {
      

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

        internal TimeBoundAccessRequest CheckForValidTimeBoundAccessRequest(SystemUser user)
        {
            TimeBoundAccessRequest tbar = new TimeBoundAccessRequest();
            try
            {
                if (!user.IsValid())
                {
                    tbar.SetFailuresAsStatusInResponseFields(user.StatusDesc);
                    return tbar;
                }

                TimeBoundAccessRequest[] all = TimeBoundAccessRequest.QueryWithStoredProc("GetTimeBoundRequestByUserId", user.Username);

                if (all.Count() <= 0)
                {
                    tbar.StatusCode = Globals.FAILURE_STATUS_CODE;
                    tbar.StatusDesc = $"NO APPROVED TIME BOUND ACCESS REQUEST FOUND";
                    return tbar;
                }

                tbar = all[0];
                tbar.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                tbar.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return tbar;
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

                if (req.Status != "PENDING")
                {
                    apiResult.PegPayID = req.Id.ToString();
                    apiResult.SetSuccessAsStatusInResponseFields();
                    return apiResult;
                }

                ApiResult sendResult = SendApproveTbarEmail(req);

                if (sendResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    apiResult.PegPayID = req.Id.ToString();
                    apiResult.SetFailuresAsStatusInResponseFields("FAILED TO SEND EMAIL TO APPROVER: " + sendResult.StatusDesc);
                    return apiResult;
                }


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

        internal ApiResult SaveSystemSetting(SystemSetting settting)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!settting.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(settting.StatusDesc);
                    return apiResult;
                }

                SystemSetting old = SystemSetting.QueryWithStoredProc("GetSystemSettingByID", settting.SettingKey).FirstOrDefault();

                settting.Id = old != null ? old.Id : settting.Id;

                settting.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AttachSystemAffectedToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult SaveSystemType(SystemType settting)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                if (!settting.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(settting.StatusDesc);
                    return apiResult;
                }

                SystemType old = SystemType.QueryWithStoredProc("GetSystemTypeByID", settting.TypeCode).FirstOrDefault();

                settting.Id = old != null ? old.Id : settting.Id;

                settting.Save();
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

                //TimeBoundAccessRequest tbar = new TimeBoundAccessRequest();
                //tbar.Approver = "nsubugak";
                //tbar.ApproverReason = "Test";
                //tbar.DurationInMinutes = 15;
                //tbar.Reason = "To Test";
                //tbar.StartTime = DateTime.Now;
                //tbar.Status = "APPROVED";
                //tbar.SystemCode = "ChangeRequestDB";
                //tbar.TBPAccessId = DateTime.Now.Ticks.ToString();
                //tbar.TypeOfAccess = "FULL";
                //tbar.UserId = "nsubugak";

                //SaveTimeBoundAccessRequest(tbar);

                SystemSetting setting = new SystemSetting();
                setting.SettingKey = Globals.FILE_PATH_TO_APPROVE_CR_EMAIL_TEMPLATE;
                setting.SettingValue= @"E:\PePay\ChangeRequestProject\ChangeRequestAPI\ChangeRequestSubSystem\ApproveChangeRequestEmail.html";

                SaveSystemSetting(setting);

                setting = new SystemSetting();
                setting.SettingKey = Globals.FILE_PATH_TO_APPROVE_TBAR_EMAIL_TEMPLATE;
                setting.SettingValue = @"E:\PePay\ChangeRequestProject\ChangeRequestAPI\ChangeRequestSubSystem\ApproveTBAREmail.html";

                SaveSystemSetting(setting);

                setting = new SystemSetting();
                setting.SettingKey = Globals.APPROVE_CR_URL;
                setting.SettingValue = @"localhost:25235/ApproveChangeRequest.aspx";

                SaveSystemSetting(setting);

                setting = new SystemSetting();
                setting.SettingKey = Globals.APPROVE_TBAR_URL;
                setting.SettingValue = @"localhost:25235/ApproveTbarRequest.aspx";

                SaveSystemSetting(setting);

                PegasusSystem system = new PegasusSystem();
                system.ConnectionString = "Data Source=(local);Initial Catalog=TestMerchantCoreDB;User Id=sa;Password=T3rr1613;";
                system.CreatedBy = "nsubugak";
                system.CreatedOn = DateTime.Now;
                system.ModifiedBy = "nsubugak";
                system.ModifiedOn = DateTime.Now;
                system.SystemCode = "TestMerchantCoreDB";
                system.SystemName = "TestMerchantCoreDB";

                SavePegasusSystem(system);

                system = new PegasusSystem();
                system.ConnectionString = "Data Source=(local);Initial Catalog=TestGenericPegPayApi;User Id=sa;Password=T3rr1613;";
                system.CreatedBy = "nsubugak";
                system.CreatedOn = DateTime.Now;
                system.ModifiedBy = "nsubugak";
                system.ModifiedOn = DateTime.Now;
                system.SystemCode = "TestGenericPegPayApi";
                system.SystemName = "TestGenericPegPayApi";
                system.SystemType = "DATABASE";

                SavePegasusSystem(system);

                system = new PegasusSystem();
                system.ConnectionString = "Data Source=(local);Initial Catalog=ChangeRequestDB;User Id=sa;Password=T3rr1613;";
                system.CreatedBy = "nsubugak";
                system.CreatedOn = DateTime.Now;
                system.ModifiedBy = "nsubugak";
                system.ModifiedOn = DateTime.Now;
                system.SystemCode = "ChangeRequestDB";
                system.SystemName = "ChangeRequestDB";
                system.SystemType = "DATABASE";

                SavePegasusSystem(system);

                SystemType type = new SystemType();
                type.CreatedBy = "nsubugak";
                type.CreatedOn = DateTime.Now;
                type.ModifiedBy = "nsubugak";
                type.ModifiedOn = DateTime.Now;
                type.TypeCode = "DATABASE";
                type.TypeName = "Database";

                SaveSystemType(type);

                type = new SystemType();
                type.CreatedBy = "nsubugak";
                type.CreatedOn = DateTime.Now;
                type.ModifiedBy = "nsubugak";
                type.ModifiedOn = DateTime.Now;
                type.TypeCode = "FIREWALL";
                type.TypeName = "Database";

                SaveSystemType(type);

                type = new SystemType();
                type.CreatedBy = "nsubugak";
                type.CreatedOn = DateTime.Now;
                type.ModifiedBy = "nsubugak";
                type.ModifiedOn = DateTime.Now;
                type.TypeCode = "SERVER";
                type.TypeName = "SERVER";

                SaveSystemType(type);

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
                if (!Globals.AcceptableMethodsOfSendingOTP.Contains(MethodOfSending.ToUpper()))
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
                apiResult.StatusDesc = $"Successfully Sent One time Password by {MethodOfSending} to {sendResult.PegPayID}. Its Valid for {oneTimePassword.ValidityDurationInSeconds / 60} minute(s)";
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

        internal DataSet ExecuteSqlQuery(string SqlQuery, TimeBoundAccessRequest tbar)
        {
            try
            {
                PegasusSystem system = PegasusSystem.QueryWithStoredProc("GetSystemById", tbar.SystemCode).FirstOrDefault();

                if (system == null)
                {
                    throw new Exception($"UNABLE TO EXECUTE QUERY ON {tbar.SystemCode} AT THIS TIME");
                }

                ThirdPartyDB thirdPartyDB = new ThirdPartyDB(system.ConnectionString);
                DataSet resultSet = thirdPartyDB.ExecuteSqlQuery(SqlQuery);

                DbQueryLog dbQueryLog = new DbQueryLog();
                dbQueryLog.CreatedOn = DateTime.Now;
                dbQueryLog.CreatedBy = tbar.UserId;
                dbQueryLog.ModifiedBy = tbar.UserId;
                dbQueryLog.ModifiedOn = DateTime.Now;
                dbQueryLog.NumberOfRowsAffected = $"Tables Returned = {resultSet?.Tables?.Count}, Rows Returned = {resultSet?.Tables?[0]?.Rows?.Count}";
                dbQueryLog.QuerySql = SqlQuery;
                dbQueryLog.RecordDate = DateTime.Now;
                dbQueryLog.UserId = tbar.UserId;

                //save db query async
                Task.Factory.StartNew(() =>
                {
                    dbQueryLog.SaveLog();
                });

                return resultSet;
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

        public ApiResult SendApproveCrEmail(ApproverToChangeRequestLink link)
        {
            ApiResult result = new ApiResult();
            try
            {
                SystemUser approver = SystemUser.QueryWithStoredProc("GetSystemUserById", link.UserId).FirstOrDefault();

                if (approver == null)
                {
                    result.StatusCode = Globals.FAILURE_STATUS_CODE;
                    result.StatusDesc = $"USER WITH THE APPROVER ID {link.UserId} WAS NOT FOUND";
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
                msg = msg.Replace("[USER_ID]", link.UserId);
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

        public ApiResult SendApproveTbarEmail(TimeBoundAccessRequest tbar)
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

        internal int ExecuteNonQuery(string SqlQuery, TimeBoundAccessRequest tbar)
        {
            try
            {
                PegasusSystem system = PegasusSystem.QueryWithStoredProc("GetSystemById", tbar.SystemCode).FirstOrDefault();

                if (system == null)
                {
                    throw new Exception($"UNABLE TO EXECUTE QUERY ON {tbar.SystemCode} AT THIS TIME");
                }

                ThirdPartyDB thirdPartyDB = new ThirdPartyDB(system.ConnectionString);
                int rowsAffected = thirdPartyDB.ExecuteNonQuery(SqlQuery);

                DbQueryLog dbQueryLog = new DbQueryLog();
                dbQueryLog.CreatedOn = DateTime.Now;
                dbQueryLog.CreatedBy = tbar.UserId;
                dbQueryLog.ModifiedBy = tbar.UserId;
                dbQueryLog.ModifiedOn = DateTime.Now;
                dbQueryLog.NumberOfRowsAffected = "Rows Affected = " + rowsAffected;
                dbQueryLog.QuerySql = SqlQuery;
                dbQueryLog.RecordDate = DateTime.Now;
                dbQueryLog.UserId = tbar.UserId;

                //save db query async
                Task.Factory.StartNew(() =>
                {
                    dbQueryLog.SaveLog();
                });

                return rowsAffected;
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

        internal ApiResult SendOneTimePINByEmail(string preferedContact, string OTP)
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

        internal ApiResult HandleException(string methodName, string message, Exception ex)
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

        internal ApiResult ApproveChangeRequest(string UserId, string ChangeRequestId, string Decision)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                ApproverToChangeRequestLink old = ApproverToChangeRequestLink.QueryWithStoredProc("GetApproverByID", UserId, ChangeRequestId, "").FirstOrDefault();

                if (old==null)
                {
                    apiResult.SetFailuresAsStatusInResponseFields($"NOT ABLE TO FIND CR {ChangeRequestId} FOR APPROVER {UserId}");
                    return apiResult;
                }

                old.Decision = Decision;
                old.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AssignApproverToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult ApproveTBAR(string UserId, string TbarId, string Decision)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                TimeBoundAccessRequest old = TimeBoundAccessRequest.QueryWithStoredProc("GetTBarByID", UserId,TbarId).FirstOrDefault();

                if (old == null)
                {
                    apiResult.SetFailuresAsStatusInResponseFields($"NOT ABLE TO FIND TBAR {TbarId} FOR APPROVER {UserId}");
                    return apiResult;
                }

                old.Status = Decision;
                old.ApproverReason = Decision;
                old.Save();
                apiResult.SetSuccessAsStatusInResponseFields();
            }
            catch (Exception ex)
            {
                HandleError(nameof(AssignApproverToChangeRequest), "EXCEPTION", ex.Message);
                apiResult.SetFailuresAsStatusInResponseFields(ex.Message);
            }
            return apiResult;
        }

        internal ApiResult AssignApproverToChangeRequest(ApproverToChangeRequestLink link)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                link.HasEmailBeenSent = false;
                if (!link.IsValid())
                {
                    apiResult.SetFailuresAsStatusInResponseFields(link.StatusDesc);
                    return apiResult;
                }
                
                ApproverToChangeRequestLink old = ApproverToChangeRequestLink.QueryWithStoredProc("GetApproverByID",link.UserId,link.ChangeRequestId,link.Role).FirstOrDefault();

                link.Id = old != null ? old.Id : link.Id;
                link.Save();

                
                ApiResult sendResult = SendApproveCrEmail(link);

                if (sendResult.StatusCode != Globals.SUCCESS_STATUS_CODE)
                {
                    apiResult.PegPayID = link.Id.ToString();
                    apiResult.SetFailuresAsStatusInResponseFields("FAILED TO SEND EMAIL TO APPROVER: "+sendResult.StatusDesc);
                    return apiResult;
                }

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

        internal ApiResult SavePegasusSystem(PegasusSystem req)
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

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
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
                    typeof(TimeBoundAccessRequest),
                    typeof(DbQueryLog),
                    typeof(SystemSetting),
                    typeof(PegasusSystem),
                    typeof(SystemType)
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

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

       
    }
}
