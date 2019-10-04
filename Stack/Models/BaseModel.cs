using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack.Models
{
    public class BaseModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastEditedById { get; set; }
        public DateTime LastEditedDate { get; set; }
        public string ProfilePicUrl { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}