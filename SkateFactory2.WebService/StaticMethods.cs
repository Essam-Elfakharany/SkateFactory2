using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SkateFactory2.WebService
{
    public static class StaticMethods
    {
        public static string GetConnectionString(System.Web.Services.WebService origin)
        {
            string result = ConfigurationManager.ConnectionStrings["SkateboardCN"].ConnectionString;
            result = result.Replace("{APP_DATA_PATH}", origin.Server.MapPath("~/App_Data"));
            return result;
        }
    }
}