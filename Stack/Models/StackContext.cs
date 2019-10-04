using System.Data.Entity;
using System.Web.UI.WebControls;
using Stack.DBModels;

namespace Stack.Models
{
    public class StackContext : DbContext
    {
        public StackContext()
            : base("StackDBContext")
        { }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Questions> Questions { get; set; }
        //public DbSet<ArticleCategories> ArticleCategories { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<ProjectFiles> ProjectFiles { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Articles>()
        //     .Map(m =>
        //     {
        //         m.Properties(p => new { p.MenuItemId});
        //         m.ToTable("Articles");
        //     })
        //     .Map(m =>
        //     {
        //         m.Properties(p => new { p.MenuItemId});
        //         m.ToTable("MenuItems");
        //     });
        //}
    }
}