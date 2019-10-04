using System;
using System.Collections.Generic;
using System.Linq;
using Stack.DBModels;

namespace Stack.Models
{
    public class ArticlesModel : BaseModel
    {
        public int ArticleId { get; set; }
        public int ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string ArticleCategoryUrl { get; set; }

        public string ArticleName { get; set; }
        public string ArticleNameSeo { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ProfilePicUrl { get; set; }
        public string MenuItemSeo { get; set; }

        public List<Articles> ArticlesList { get; set; }
        public List<Articles> GetRecentArticles()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Articles.Take(6).ToList();
            }
        }
        public List<Articles> GetArticlesOnMenuItemId(string menuItemId, int pageNumber)
        {
            using (var ctx = new StackContext())
            {
                int skipRecords = (pageNumber != 0 ? pageNumber - 1 : pageNumber) * 5;
                ArticlesList = ctx.Articles.Where(m => m.MenuItems.MenuItemSeo == menuItemId).ToList();
                TotalRecords = ArticlesList.Count;
                ArticlesList = ArticlesList.OrderBy(m => m.ArticleName).Skip(skipRecords).Take(5).ToList();
                return ArticlesList;
            }
        }
        public void Edit(string id)
        {
            //var userDetails = UserDetails.Instance;
            using (var ctx = new StackContext())
            {
                var article = ctx.Articles.ToList().Find(x => x.ArticleNameSeo == id);
                ArticleId = article.ArticleId;
                ArticleName = article.ArticleName;
                ArticleNameSeo = article.ArticleNameSeo;
                Description = article.Description;
                CreatedBy = article.CreatedBy;
                LastEditedDate = article.LastEditedDate;
                LastEditedById = article.LastEditedById;
                //ProfilePicUrl = userDetails.ProfilePicUrl;
            }
        }
    }
}