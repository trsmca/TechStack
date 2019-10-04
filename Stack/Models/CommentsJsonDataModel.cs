using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack.Models
{
    public class CommentsJsonDataModel
    {
        public int id { get; set; }
        public string parent { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string content { get; set; }
        public string pings { get; set; }
        public int creator { get; set; }
        public string fullname { get; set; }
        public string profile_picture_url { get; set; }
        public bool created_by_admin { get; set; }
        public bool created_by_current_user { get; set; }
        public int upvote_count { get; set; }
        public bool user_has_upvoted { get; set; }
    }
}