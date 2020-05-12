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
        public string UploadUserProfilePic()
        {
            string FileName = "";
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                //string filename = Path.GetFileName(Request.Files[i].FileName);  

                HttpPostedFileBase file = files[i];
                string fname;

                // Checking for Internet Explorer  
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                    FileName = file.FileName;
                }

                using (Stream fs = file.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        int userId = Convert.ToInt32(Session["UserId"]);
                        _model.UploadUserProfilePic(userId, bytes);
                        return "Profile picture updated successfully,";
                    }
                }
                // Get the complete folder path and store the file inside it.  
                //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                //file.SaveAs(fname);
            }
            return string.Empty;
        }

        public ActionResult LoadUserProfilePic()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            ImageModel image = _model.GetImages(userId);
            if (image != null)
            {
                image.IsSelected = true;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Profile()
        {
            object value = Session["UserId"];
            if (value != null)
            {
                int userId = Convert.ToInt32(value);
                _model.GetUsers(userId);

                ImageModel image = _model.GetImages(userId);
                if (image.Data != null)
                {
                    image.IsSelected = true;
                    ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
                }
            }
            return View(_model);
        }
        [AllowAnonymous]
        public ActionResult ProfileEdit()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            _model.GetUsers(userId);
            return View(_model);
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
