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
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace Stack.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private StackContext db = new StackContext();
        private AdminProjectsModel _model = new AdminProjectsModel();
        // GET: Admin/Projects
        public ActionResult Index()
        {
            //var Projects = db.Projects.Include(a => a.ArticleCategory);
            var Projects = db.Projects.ToList();
            return View(Projects);
        }

        //// GET: Admin/Projects/Details/5
        //public ActionResult Details(int? id)
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

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            //ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1");
            return View(_model);
        }

        //// POST: Admin/Projects/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(AdminProjectsModel model)
        {
            //if (ModelState.IsValid)
            //{
            string fileName = string.Empty;
            model.Save();
            _model.ProjectId = model.ProjectId;
            //var filePaths = DirSearch("C:\\Raja Sekhar\\Projects\\barter\\web\\");
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    fileName = model.ProjectId + "__" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Documents/Projects/Abstract/"), fileName);
                    model.SaveAttachments(fileName, "Abstratct");
                    file.SaveAs(path);
                }
                var projectFile = Request.Files[1];
                if (projectFile != null && projectFile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(projectFile.FileName);
                    fileName = model.ProjectId + "__" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Documents/Projects/Project/"), fileName);
                    projectFile.SaveAs(path);
                    model.SaveAttachments(fileName, "Projects");
                    Directory.CreateDirectory(Server.MapPath("~/Documents/Projects/Extract/Project_" + model.ProjectId));
                    ZipFile.ExtractToDirectory(path, Server.MapPath("~/Documents/Projects/Extract/Project_" + model.ProjectId));
                }
            }
            return RedirectToAction("Index");
            //return View(_model);
        }
        int parentId = 0;
        bool count = false;
        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    count = true;
                    files.Add(f);
                    _model.SaveProjectFiles(0, _model.ProjectId, parentId, f.ToString());
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    if (count == true)
                        parentId = 0;
                    parentId = _model.SaveProjectFiles(0, _model.ProjectId, parentId, d.ToString());
                    count = false;
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                //MessageBox.Show(excpt.Message);
            }

            return files;
        }
        // GET: Admin/Projects/Edit/5
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

        //// POST: Admin/Projects/Edit/5
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

        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            _model.Delete(id);
            var Projects = db.Projects.ToList();
            return View("Index", Projects);
        }

        //// POST: Admin/Projects/Delete/5
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
