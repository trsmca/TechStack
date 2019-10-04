using System;
using System.Collections.Generic;
using System.Linq;
using Stack.DBModels;
using System.Data.Entity.Migrations;

namespace Stack.Models
{
    public class AttachmentsModel
    {
        public int AttachmentId { get; set; }
        public int PkId { get; set; }
        public string AttachmentCategory { get; set; }
        public string AttachmentKey { get; set; }
        public byte Attachment { get; set; }

        public static void SaveAttachments(string fileName, string AttachmentKey, int pkId)
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var attachment = new Attachments
                {
                    PkId = pkId,
                    AttachmentCategory = AttachmentKey,
                    AttachmentKey = AttachmentKey,
                    FileName = fileName,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    CreatedById = userDetails.UserId,
                    //CreatedBy = userDetails.FullName,
                    LastEditedById = userDetails.UserId
                };
                //ctx.Entry(Project).State = EntityState.Modified;
                ctx.Attachments.AddOrUpdate(attachment);
                ctx.SaveChanges();
            }
        }
    }
}