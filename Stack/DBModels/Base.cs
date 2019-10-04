using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stack.DBModels
{
    public class Base : DbSet
    {
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastEditedById { get; set; }
        public DateTime LastEditedDate { get; set; }

    }
}