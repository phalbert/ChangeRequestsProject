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
        public static string ConnectionString = "TestPegPayConnectionString";
        private static Database DB = DatabaseFactory.CreateDatabase(ConnectionString);
        private DbCommand procommand;
        

        public DatabaseHandler()
        {
           
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

        public static DataTable ExecuteStoredProc(string StoredProc, params object[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                DbCommand procommand = DB.GetStoredProcCommand(StoredProc, parameters);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet ExecuteSqlQuery(string sqlQuery)
        {
            DataSet dt = new DataSet();
            try
            {
                DbCommand procommand = DB.GetSqlStringCommand(sqlQuery);
                dt = DB.ExecuteDataSet(procommand);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ExecuteNonQuery(string sqlQuery)
        {
            int dt = 0;
            try
            {
                DbCommand procommand = DB.GetSqlStringCommand(sqlQuery);
                dt = DB.ExecuteNonQuery(procommand);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<dynamic> ExecuteDynamicDataTable(string storedProc, params object[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                DbCommand procommand = DB.GetStoredProcCommand(storedProc, parameters);
                dt = DB.ExecuteDataSet(procommand).Tables[0];
                List<dynamic> dys = dt.AsDynamicEnumerable().ToList();
                return dys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        internal static DataSet ExecuteDataSet(string storedProc, object[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                DbCommand procommand = DB.GetStoredProcCommand(storedProc, parameters);
                ds = DB.ExecuteDataSet(procommand);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}
