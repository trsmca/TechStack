using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stack.DBModels
{
    public class Questions : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        //public int MenuItemId { get; set; }
        public string Question { get; set; }
        public string QuestionSeo { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}