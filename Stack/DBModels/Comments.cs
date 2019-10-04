using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Stack.DBModels
{
    public class Comments : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        
        public int UserId { get; set; }
        public int PkId { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public int ParentId { get; set; }
        public string Pings { get; set; }
        //public string ProfilePictureUrl { get; set; }
        public int UpvoteCount { get; set; }
        public bool UserHasUpvoted { get; set; }
        public string CreatedBy { get; set; }
         
    }
}