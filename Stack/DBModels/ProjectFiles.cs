using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.DynamicData;

namespace Stack.DBModels
{
    public class ProjectFiles : DbSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectFileId { get; set; }
        public int ProjectId { get; set; }
        public int ParentId { get; set; }
        public string FileName { get; set; } 
    }
}