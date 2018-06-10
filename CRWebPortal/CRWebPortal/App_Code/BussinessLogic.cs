using System;
using CRWebPortal.CRSystemAPI;

namespace CRWebPortal
{
    public class BussinessLogic
    {
        public static CRSystemAPIClient cRSystemAPIClient = new CRSystemAPIClient();

        public static CRSystemAPIClient GetCRSystemAPIHandle()
        {
            return cRSystemAPIClient;
        }

        internal static string GenerateUniqueId(string v)
        {
            try
            {
                return v + DateTime.Now.Ticks.ToString();
            }
            catch (Exception)
            {
                return v+"UNKNOWN";
            }
        }
    }
}