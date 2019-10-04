using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Stack.DBModels
{
    [Table("Articles")]
    public class Articles : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }

        //[ForeignKey("MenuItems")]
        public int MenuItemId { get; set; }


        public string ArticleNameSeo { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string CreatedBy { get; set; }

        public virtual MenuItems MenuItems { get; set; }

    }
}