using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack.Areas.Admin.Models;
using Stack.DBModels;
using Stack.Helpers;
using Stack.Models;

namespace Stack.Areas.Admin.Controllers
{
    public class ArticlesController : Controller
    {
        private StackContext db = new StackContext();
        private AdminArticlesModel _model = new AdminArticlesModel();
        // GET: Admin/Articles
        public ActionResult Index()
        {
            //var articles = db.Articles.Include(a => a.ArticleCategory);
            var articles = db.Articles.ToList();
            return View(articles);
        }

        //// GET: Admin/Articles/Details/5
        //public ActionResult Details(int? id)
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

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            //ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1");
            return View(_model);
        }

        //// POST: Admin/Articles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(AdminArticlesModel model)
        {
            if (ModelState.IsValid)
            {
                int articleId = model.Save();
                foreach (HttpPostedFileBase file in model.CoverPhoto)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);

                        AttachmentsModel.SaveAttachments(InputFileName, ConvertToByte(file), "ArticlesCoverPhoto", articleId);
                        ViewBag.UploadStatus = model.files.Count().ToString() + " Cover Photo uploaded successfully.";
                    }

                }
                foreach (HttpPostedFileBase file in model.files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);

                        AttachmentsModel.SaveAttachments(InputFileName, ConvertToByte(file), "Articles", articleId);
                        ViewBag.UploadStatus = model.files.Count().ToString() + " files uploaded successfully.";
                    }

                }
                return RedirectToAction("Index");
            }
            return View(_model);
        }

        // GET: Admin/Articles/Edit/5
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

        //// POST: Admin/Articles/Edit/5
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

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            _model.Delete(id);
            var articles = db.Articles.ToList();
            return View("Index", articles);
        }

        //// POST: Admin/Articles/Delete/5
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
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }
    }
}
