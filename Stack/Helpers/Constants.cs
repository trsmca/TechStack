using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace Stack.Helpers
{
    public class Constants
    {
        public static readonly string emailFromAddress = ConfigurationManager.AppSettings["emailFromAddress"];
        public static readonly string smtpAddress = ConfigurationManager.AppSettings["smtpAddress"];
        public static readonly int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["portNumber"]);
        public static readonly string password = ConfigurationManager.AppSettings["password"];
    }
}