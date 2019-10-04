using Stack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Stack.Controllers
{
    public class CommentsController : Controller
    {
        private CommentsModel _model = new CommentsModel();
        // GET: Comments
        public ActionResult Index(int pkid, string category)
        {
            //List<CommentsModel> people = new List<CommentsJsonDataModel>{
            //       new CommentsJsonDataModel{id = 1, parent = 1, CreatedDate= DateTime.Now},
            //       new CommentsJsonDataModel{id= 2, parent = 1, CreatedDate = DateTime.Now}
            //       };

            //string jsonString = people.ToJSON();
            _model.GetComments(pkid, category);
            return View();
        }
        public ActionResult Get(int pkid, string category)
        {
            //List<CommentsModel> people = new List<CommentsJsonDataModel>{
            //       new CommentsJsonDataModel{id = 1, parent = 1, CreatedDate= DateTime.Now},
            //       new CommentsJsonDataModel{id= 2, parent = 1, CreatedDate = DateTime.Now}
            //       };

            //string jsonString = people.ToJSON();
            return Json(_model.GetComments(pkid, category), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(CommentsModel model)
        {
            model.Save();
            return View();
        }
    }
}