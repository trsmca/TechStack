using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack;
using Stack.Common;
using Stack.Models;

namespace Stack.Areas.Admin.Controllers
{
    public class ArticleCategoriesController : Controller
    {
        private StackContext db = new StackContext();

        //// GET: Admin/ArticleCategories
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.ArticleCategories.ToListAsync());
        //}

        //// GET: Admin/ArticleCategories/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ArticleCategory articleCategory = await db.ArticleCategories.FindAsync(id);
        //    if (articleCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(articleCategory);
        //}

        //// GET: Admin/ArticleCategories/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/ArticleCategories/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "ArticleCategoryId,ArticleCategory1")] ArticleCategory articleCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ArticleCategories.Add(articleCategory);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(articleCategory);
        //}

        //// GET: Admin/ArticleCategories/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ArticleCategory articleCategory = await db.ArticleCategories.FindAsync(id);
        //    if (articleCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(articleCategory);
        //}

        //// POST: Admin/ArticleCategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ArticleCategoryId,ArticleCategory1")] ArticleCategory articleCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        articleCategory.URL = StackHelper.ConvertToSeoFriendly(articleCategory.ArticleCategory1);
        //        db.Entry(articleCategory).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(articleCategory);
        //}

        //// GET: Admin/ArticleCategories/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ArticleCategory articleCategory = await db.ArticleCategories.FindAsync(id);
        //    if (articleCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(articleCategory);
        //}

        //// POST: Admin/ArticleCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ArticleCategory articleCategory = await db.ArticleCategories.FindAsync(id);
        //    db.ArticleCategories.Remove(articleCategory);
        //    await db.SaveChangesAsync();
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
