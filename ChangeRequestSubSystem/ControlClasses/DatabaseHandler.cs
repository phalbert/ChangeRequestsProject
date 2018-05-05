using ChangeRequestSubSystem.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.ControlClasses
{
    public class DatabaseHandler
    {
        private Database DB;
        private DbCommand procommand;
        public string ConnectionString = "TestPegPayConnectionString";

        public DatabaseHandler()
        {
            try
            {
                DB = DatabaseFactory.CreateDatabase(ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogError(string ID, string Message)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        internal DataTable GetUserByID(string username)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("GetSystemUserbyID", username);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveOneTimePIN(string username, string OTP)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("SaveOneTimePassword", username, OTP);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataSet CheckUserCreds(string username, string password)
        {
            DataSet dt = new DataSet();
            try
            {
                procommand = DB.GetStoredProcCommand("CheckUserCreds", username, password);
                dt = DB.ExecuteDataSet(procommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveChangeRequest(ChangeRequest changeRequest)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("SaveChangeRequests",
                                                      changeRequest.ChangeRequestId,
                                                      changeRequest.Title,
                                                      changeRequest.RequesterId,
                                                      changeRequest.RequesterAddress,
                                                      changeRequest.RequesterEmail,
                                                      changeRequest.RequesterPhone,
                                                      changeRequest.Implementer,
                                                      changeRequest.ImplementerEmail,
                                                      changeRequest.ImplementerPhone,
                                                      changeRequest.ImplementationDate,
                                                      changeRequest.Description,
                                                      changeRequest.Justification,
                                                      changeRequest.ImpactOfNotImplementing,
                                                      changeRequest.ChangeCategoryCode);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataSet ExecuteDataSet(string storedProc, object[] parameters)
        {
            try
            {
                procommand = DB.GetStoredProcCommand(storedProc, parameters);
                DataSet ds = DB.ExecuteDataSet(procommand);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable SaveCompany(Company req)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("SaveCompany", req.CompanyName, req.CompanyCode, req.ModifiedBy);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveRole(Role req)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("SaveRole", req.RoleName, req.RoleCode, req.CompanyCode, req.ModifiedBy);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable AssignChangeRequestToApprover(ChangeRequest changeRequest, Approver approver)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("SaveApproverForAChangeRequest", changeRequest.ChangeRequestId, approver.Username, approver.RoleCode, "PENDING", "PENDING");
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable UpdateChangeRequestStatus(ChangeRequest changeRequest, Approver approver, string Decision)
        {
            DataTable dt = new DataTable();
            try
            {
                procommand = DB.GetStoredProcCommand("UpdateChangeRequestStatus", changeRequest.ChangeRequestId, approver.Username, Decision);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
