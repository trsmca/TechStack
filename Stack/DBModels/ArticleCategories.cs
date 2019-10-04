using System.ComponentModel.DataAnnotations;

namespace Stack.DBModels
{
    public class ArticleCategories : Base
    {
        [Key]
        public int ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }

        //public string ArticleCategory { get; set; }
    }
}