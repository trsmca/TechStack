using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Stack.DBModels
{
    public class Projects : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public int MenuItemId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNameSeo { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public virtual MenuItems MenuItems { get; set; }
    }
}