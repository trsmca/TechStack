using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Stack.Common
{
    public static class StackHelper
    {
        public static string ConvertToSeoFriendly(string text)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            text = System.Text.Encoding.ASCII.GetString(bytes);
            string str = text.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
    }
}