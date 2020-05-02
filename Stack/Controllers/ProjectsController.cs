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
    public class ProjectsController : Controller
    {
        private StackContext db = new StackContext();
        private readonly ProjectsModel _model = new ProjectsModel();
        // GET: Projects
        [Route("Projects/{id}")]
        public ActionResult Index(string id)
        {
            _model.GetProjectsOnMenuItemId(id, 0);
            _model.MenuItemSeo = id;
            return View(_model);
        }
        public ActionResult Index(string id, int pageNumber)
        {
            _model.GetProjectsOnMenuItemId(id, pageNumber);
            return View("_Projects", _model.ProjectsList);
        }
        //// GET: RecentProjects
        //public ActionResult RecentProjects()
        //{
        //    List<Article> Projects;
        //    if (Session["ArticleCategory"] != null && Session["ArticleCategory"] != string.Empty)
        //    {
        //        string id = Session["ArticleCategory"].ToString();
        //        Projects = db.Projects.Where(d => d.ArticleCategory.ArticleCategory1 == id).ToList();
        //    }
        //    else
        //    {
        //        Projects = db.Projects.Include(a => a.ArticleCategory).ToList();
        //    }
        //    return View("_recentProjects", Projects.ToList());
        //}

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            _model.Edit(id);
            return View(_model);
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            var attachment = db.Attachments.FirstOrDefault(x => x.AttachmentId == id);
            return File(attachment.Attachment, "application/pdf", attachment.FileName);
        }
        //// GET: Projects/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1");
        //    return View();
        //}

        //// POST: Projects/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ArticleId,ArticleCategoryId,ArticleName,Description,CreatedById,CreatedDate,LastEditedById,LastEditedDate")] Article article)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Projects.Add(article);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
        //    return View(article);
        //}

        //// GET: Projects/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Projects.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
        //    return View(article);
        //}

        //// POST: Projects/Edit/5
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

        //// GET: Projects/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Projects.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        //// POST: Projects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Article article = db.Projects.Find(id);
        //    db.Projects.Remove(article);
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
