using Stack.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stack.Controllers
{
    public class AttachmentsController : Controller
    {
        private StackContext db = new StackContext();
        // GET: Attachments
        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            var attachment = db.Attachments.FirstOrDefault(x => x.AttachmentId == id);
            FileInfo fi = new FileInfo(attachment.FileName);
            return File(attachment.Attachment, attachment.ContentType, attachment.AttachmentCategory + "." + fi.Extension + "");
        }
    }
}