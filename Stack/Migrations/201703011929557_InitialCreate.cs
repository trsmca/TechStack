namespace Stack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        //ArticleCategoryId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        ArticleName = c.String(),
                        ArticleNameSeo = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(nullable: true),
                        CreatedById = c.Int(nullable: true),
                        CreatedDate = c.DateTime(nullable: true),
                        LastEditedById = c.Int(nullable: true),
                        LastEditedDate = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.ArticleId);
            //CreateTable(
            //    "dbo.ArticleCategories",
            //    c => new
            //    {
            //        ArticleCategoryId = c.Int(nullable: false),
            //        ArticleCategory = c.String(),
            //        CreatedById = c.Int(nullable: false),
            //        CreatedDate = c.DateTime(nullable: false),
            //        LastEditedById = c.Int(nullable: false),
            //        LastEditedDate = c.DateTime(nullable: false),
            //    })
            //    .PrimaryKey(t => t.ArticleCategoryId);
            CreateTable(
                "dbo.MenuItems",
                c => new
                {
                    MenuItemId = c.Int(nullable: false, identity: true),
                    ParentMenuId = c.Int(nullable: false),
                    MenuItem = c.String(),
                    MenuItemSeo = c.String(),
                    MenuUrl = c.String(),
                    CreatedById = c.Int(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    LastEditedById = c.Int(nullable: true),
                    LastEditedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.MenuItemId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    Email = c.String(nullable: true),
                    Password = c.String(nullable: true),
                    ContactNumber = c.String(),
                    FirstName = c.String(),
                    LastName = c.String(nullable: true),
                    Gender = c.Boolean(nullable: true),
                    Address = c.String(),
                    State = c.String(),
                    Country = c.String(),
                    PinCode = c.String(),
                    ProfilePicUrl = c.String(),
                    Description = c.String(),
                    IsActive = c.Boolean(),
                    CreatedDate = c.DateTime(nullable: true),
                    CreatedById = c.Int(nullable: true),
                    LastEditedById = c.Int(nullable: true),
                    LastEditedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    CommentId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    PkId = c.Int(nullable: false),
                    Category = c.String(),
                    Comment = c.String(),
                    ParentId = c.Int(nullable: true),
                    Pings = c.String(),
                    //ProfilePictureUrl = c.String(),
                    //CreatedByAdmin = c.String(nullable: true),
                    UpvoteCount = c.Int(nullable: true),
                    UserHasUpvoted = c.Boolean(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    CreatedById = c.Int(nullable: true),
                    CreatedBy = c.String(nullable: true),
                    LastEditedById = c.Int(nullable: true),
                    LastEditedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.CommentId);

            CreateTable(
                "dbo.Projects",
                c => new
                {
                    ProjectId = c.Int(nullable: false, identity: true),
                    MenuItemId = c.Int(nullable: false),
                    ProjectName = c.String(),
                    ProjectNameSeo = c.String(),
                    Description = c.String(),
                    CreatedById = c.Int(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    LastEditedById = c.Int(nullable: true),
                    LastEditedDate = c.DateTime(nullable: true)
                })
                .PrimaryKey(t => t.ProjectId);
            CreateTable(
                "dbo.Questions",
                c => new
                {
                    QuestionId = c.Int(nullable: false, identity: true),
                    Question = c.String(),
                    QuestionSeo = c.String(),
                    Description = c.String(),
                    CreatedById = c.Int(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    LastEditedById = c.Int(nullable: true),
                    LastEditedDate = c.DateTime(nullable: true)
                })
                .PrimaryKey(t => t.QuestionId);

            CreateTable(
               "dbo.Attachments",
               c => new
               {
                   AttachmentId = c.Int(nullable: false, identity: true),
                   PkId = c.Int(nullable: false),
                   AttachmentCategory = c.String(),
                   AttachmentKey = c.String(),
                   Attachment = c.Binary(),
                   CreatedById = c.Int(nullable: true),
                   CreatedDate = c.DateTime(nullable: true),
                   LastEditedById = c.Int(nullable: true),
                   LastEditedDate = c.DateTime(nullable: true)
               })
               .PrimaryKey(t => t.AttachmentId);
        }

        public override void Down()
        {
            DropTable("dbo.Articles");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.Questions");
        }
    }
}
