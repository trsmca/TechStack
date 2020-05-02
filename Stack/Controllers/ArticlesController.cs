using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack;
using Stack.DBModels;
using Stack.Models;

namespace Stack.Controllers
{
    public class ArticlesController : Controller
    {
        private StackContext db = new StackContext();
        private readonly ArticlesModel _model = new ArticlesModel();
        // GET: Articles
        [Route("Articles/{id}")]
        public ActionResult Index(string id)
        {
            _model.GetArticlesOnMenuItemId(id, 0);
            _model.MenuItemSeo = id;
            return View(_model);
        }
        public ActionResult Index(string id, int? pageNumber)
        {
            _model.GetArticlesOnMenuItemId(id,Convert.ToInt32(pageNumber));
            return View("_articles", _model.ArticlesList);
        }
        //// GET: RecentArticles
        //public ActionResult RecentArticles()
        //{
        //    List<Article> articles;
        //    if (Session["ArticleCategory"] != null && Session["ArticleCategory"] != string.Empty)
        //    {
        //        string id = Session["ArticleCategory"].ToString();
        //        articles = db.Articles.Where(d => d.ArticleCategory.ArticleCategory1 == id).ToList();
        //    }
        //    else
        //    {
        //        articles = db.Articles.Include(a => a.ArticleCategory).ToList();
        //    }
        //    return View("_recentArticles", articles.ToList());
        //}

        // GET: Articles/Details/5
        public ActionResult Details(string id)
        {
            _model.Edit(id);
            return View(_model);
        }

        //// GET: Articles/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1");
        //    return View();
        //}

        //// POST: Articles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ArticleId,ArticleCategoryId,ArticleName,Description,CreatedById,CreatedDate,LastEditedById,LastEditedDate")] Article article)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Articles.Add(article);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
        //    return View(article);
        //}

        //// GET: Articles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
        //    return View(article);
        //}

        //// POST: Articles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ArticleId,ArticleCategoryId,ArticleName,Description,CreatedById,CreatedDate,LastEditedById,LastEditedDate")] Article article)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(article).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
        //    return View(article);
        //}

        //// GET: Articles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        //// POST: Articles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Article article = db.Articles.Find(id);
        //    db.Articles.Remove(article);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
