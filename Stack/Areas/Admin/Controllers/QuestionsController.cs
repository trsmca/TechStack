using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Stack.Areas.Admin.Models;
using Stack.DBModels;
using Stack.Helpers;
using Stack.Models;

namespace Stack.Areas.Admin.Controllers
{
    public class QuestionsController : Controller
    {
        private StackContext db = new StackContext();
        private AdminQuestionsModel _model = new AdminQuestionsModel();
        // GET: Admin/Questions
        public ActionResult Index()
        {
            //var Questions = db.Questions.Include(a => a.ArticleCategory);
            var Questions = db.Questions.ToList();
            return View(Questions);
        }

        //// GET: Admin/Questions/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Article article = db.Questions.Find(id);
        //    if (article == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(article);
        //}

        // GET: Admin/Questions/Create
        public ActionResult Create()
        {
            //ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1");
            return View(_model);
        }

        //// POST: Admin/Questions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(AdminQuestionsModel model)
        {
            if (ModelState.IsValid)
            {
                model.Save();
                return RedirectToAction("Index");
            }
            return View(_model);
        }

        // GET: Admin/Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _model.Edit(id);
            //ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
            return View("Create", _model);
        }

        //// POST: Admin/Questions/Edit/5
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

        // GET: Admin/Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            _model.Delete(id);
            var Questions = db.Questions.ToList();
            return View("Index", Questions);
        }

        //// POST: Admin/Questions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Article article = db.Questions.Find(id);
        //    db.Questions.Remove(article);
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
