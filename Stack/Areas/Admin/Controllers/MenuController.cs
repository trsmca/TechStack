using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Stack.Areas.Admin.Models;
using Stack.Models;

namespace Stack.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private StackContext db = new StackContext();
        private readonly AdminMenuItems _model = new AdminMenuItems();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var menuItems = db.MenuItems.ToList();
            return View(menuItems);
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            return View(_model);
        }

        // POST: Admin/Menu/Create
        [HttpPost]
        public ActionResult Create(AdminMenuItems model)
        {
            model.Save();
            return View(model);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int id)
        {
             _model.Edit(id);
            //ViewBag.ArticleCategoryId = new SelectList(db.ArticleCategories, "ArticleCategoryId", "ArticleCategory1", article.ArticleCategoryId);
             return View("Create", _model);
        }

        // POST: Admin/Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
