using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack.Entities;

namespace Stack.Areas.Admin.Controllers
{
    public class StatusController : Controller
    {
        private TechStack db = new TechStack();

        // GET: Admin/Status
        public ActionResult Index()
        {
            return View(db.Master_Status.ToList());
        }

        // GET: Admin/Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Status master_Status = db.Master_Status.Find(id);
            if (master_Status == null)
            {
                return HttpNotFound();
            }
            return View(master_Status);
        }

        // GET: Admin/Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StatusKey,Status,IsActive")] Master_Status master_Status)
        {
            if (ModelState.IsValid)
            {
                db.Master_Status.Add(master_Status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master_Status);
        }

        // GET: Admin/Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Status master_Status = db.Master_Status.Find(id);
            if (master_Status == null)
            {
                return HttpNotFound();
            }
            return View(master_Status);
        }

        // POST: Admin/Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StatusKey,Status,IsActive")] Master_Status master_Status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master_Status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master_Status);
        }

        // GET: Admin/Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Status master_Status = db.Master_Status.Find(id);
            if (master_Status == null)
            {
                return HttpNotFound();
            }
            return View(master_Status);
        }

        // POST: Admin/Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master_Status master_Status = db.Master_Status.Find(id);
            db.Master_Status.Remove(master_Status);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
