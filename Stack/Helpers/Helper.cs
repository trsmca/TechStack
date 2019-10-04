using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Stack.Helpers
{
    public static class Helper
    {
        public static string ToSeoFriendly(string title)
        {
            var match = Regex.Match(title.ToLower(), "[\\w]+");
            StringBuilder result = new StringBuilder("");
            while (match.Success)
            {
                result.Append(match.Value + "-");
                match = match.NextMatch();
            }
            // Remove trailing '-'
            if (result[result.Length - 1] == '-') result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}