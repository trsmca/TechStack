using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack.DBModels
{
    public class MenuItems : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuItem { get; set; }
        public string MenuItemSeo { get; set; }
        public string MenuUrl { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
    }
}