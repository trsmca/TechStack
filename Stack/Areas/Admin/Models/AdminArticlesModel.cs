using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stack.DBModels;
using Stack.Models;
using Stack.Helpers;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Stack.Areas.Admin.Models
{
    public class AdminArticlesModel : AdminMenuItems
    {
        public int ArticleId { get; set; }
        public int ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string ArticleCategoryUrl { get; set; }

        public string ArticleName { get; set; }
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] CoverPhoto{ get; set; }

        public HttpPostedFileBase[] files { get; set; }

        //public List<ArticleCategories> GetArticleCategories()
        //{
        //    using (var ctx = new StackContext())
        //    {
        //        return ctx.ArticleCategories.ToList();
        //    }
        //}
        public List<Articles> GetRecentArticles()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Articles.ToList();
            }
        }
        public void Edit(int? id)
        {
            using (var ctx = new StackContext())
            {
                var article = ctx.Articles.ToList().Find(x => x.ArticleId == Convert.ToInt32(id));
                ArticleId = article.ArticleId;
                ArticleName = article.ArticleName;
                Description = article.Description;
                ShortDescription = ShortDescription;
            }
        }
        public void Delete(int? id)
        {
            using (var ctx = new StackContext())
            {
                var article = ctx.Articles.ToList().Find(x => x.ArticleId == Convert.ToInt32(id));
                ctx.Articles.Remove(article);
                ctx.SaveChanges();
            }
        }
        public int Save()
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var article = new Articles
                {
                    ArticleId = ArticleId,
                    MenuItemId = MenuItemId,
                    ArticleName = ArticleName,
                    Description = Description,
                    ShortDescription = ShortDescription,
                    ArticleNameSeo = Helper.ToSeoFriendly(ArticleName),
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    CreatedById = userDetails.UserId,
                    CreatedBy = userDetails.FullName,
                    LastEditedById = userDetails.UserId
                };
                ctx.Entry(article).State = EntityState.Modified;
                ctx.Articles.AddOrUpdate(article);
                ctx.SaveChanges();
                return article.ArticleId;
            }
            return 0;
        }
    }
}