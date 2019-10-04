using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack;
using Stack.Models;
using System.IO;

namespace Stack.Controllers
{
    public class UsersController : Controller
    {
        private StackContext db = new StackContext();
        private readonly AccountModel _model = new AccountModel();

        //// GET: Users
        public ActionResult Register()
        {
            //var users = db.Users.Include(u => u.Country).Include(u => u.Lookup).Include(u => u.State);
            return View();
        }

        //// GET: Users/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country1");
        //    ViewBag.GenderId = new SelectList(db.Lookups, "LookupId", "LokupKey");
        //    ViewBag.StateId = new SelectList(db.States, "StateId", "State1");
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserId,UserName,Password,Email,FirstName,LastName,GenderId,Address,City,StateId,CountryId,DOB,ContactNumber,Description,CreatedById,CreatedDate,LastEditedById,LastEditedDate")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country1", user.CountryId);
        //    ViewBag.GenderId = new SelectList(db.Lookups, "LookupId", "LokupKey", user.GenderId);
        //    ViewBag.StateId = new SelectList(db.States, "StateId", "State1", user.StateId);
        //    return View(user);
        //}

        // GET: Users/Edit/5
        public ActionResult Edit()
        {
            var userDetails = Stack.Models.UserDetails.Instance;
            _model.Edit(userDetails.UserId);
            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountModel _model)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(user).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            _model.Save();
            return View("Edit", _model);
        }
        [HttpPost]
        public ActionResult ProfilePic(int UserId)
        {
            string fileName = string.Empty;
            _model.UserId = UserId;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    fileName = _model.UserId + "__" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Attachments/Users/"), fileName);
                    _model.ProfilePicUrl = "/Attachments/Users/" + fileName;
                    _model.UpdateProfile();
                    file.SaveAs(path);
                }
            }
            var userDetails = Stack.Models.UserDetails.Instance;
            _model.Edit(userDetails.UserId);
            return View("Edit", _model);
        }
        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
