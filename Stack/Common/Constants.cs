using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Stack.Common
{
    public static class Constants
    {
        public static string ApplicationName { get; set; }
        public static string SupportEmail { get; set; }
         static Constants()
        {
            ApplicationName = ConfigurationManager.AppSettings["ApplicationName"].ToString();
            SupportEmail = ConfigurationManager.AppSettings["SupportEmail"].ToString();
        }
    }
}