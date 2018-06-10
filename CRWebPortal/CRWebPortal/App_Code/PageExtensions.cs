using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CRWebPortal
{
    public static class PageExtensions
    {
        public static CRSystemAPI.CRSystemAPIClient cRSystemAPIClient = new CRSystemAPI.CRSystemAPIClient();

        public static CRSystemAPI.CRSystemAPIClient GetCRSystemAPIHandle()
        {
            return cRSystemAPIClient;
        }
    }
}