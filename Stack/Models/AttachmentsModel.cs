using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Stack.DBModels;
using System.Data.Entity.Migrations;
using Microsoft.Owin.Security.Provider;
using Stack.Entities;

namespace Stack.Models
{
    public class AttachmentsModel
    {
        private static TechStack db = new TechStack();
        public int AttachmentId { get; set; }
        public int PkId { get; set; }
        public string AttachmentCategory { get; set; }
        public string AttachmentKey { get; set; }
        public byte Attachment { get; set; }
        public string FileName { get; set; }

        public static void SaveAttachments(string fileName, byte[] file, string attachmentKey, int pkId)
        {
            try
            {
                //db.Attachments.Where(x => x.AttachmentKey == attachmentKey && x.PkId == pkId).
                var userDetails = UserDetails.Instance;

                using (var ctx = new StackContext())
                {
                    //Attachments attDelete = ctx.Attachments.FirstOrDefault(x => x.AttachmentKey == attachmentKey && x.PkId == pkId);
                    //ctx.Attachments.Remove(attDelete);
                    //ctx.SaveChanges();
                    ctx.Attachments.RemoveRange(ctx.Attachments.Where(x => x.AttachmentKey == attachmentKey && x.PkId == pkId));
                    ctx.SaveChanges();

                    var attachment = new Attachments
                    {
                        PkId = pkId,
                        AttachmentCategory = attachmentKey,
                        AttachmentKey = attachmentKey,
                        FileName = fileName,
                        Attachment = file,
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
            catch (Exception ex)
            {

            }
        }
        public static void SaveAttachments(string fileName, byte[] file, string contentType, string attachmentKey, string attachmentCategory, int pkId)
        {
            try
            {
                //db.Attachments.Where(x => x.AttachmentKey == attachmentKey && x.PkId == pkId).
                var userDetails = UserDetails.Instance;

                using (var ctx = new StackContext())
                {
                    //Attachments attDelete = ctx.Attachments.FirstOrDefault(x => x.AttachmentKey == attachmentKey && x.PkId == pkId);
                    //ctx.Attachments.Remove(attDelete);
                    //ctx.SaveChanges();
                    //ctx.Attachments.RemoveRange(ctx.Attachments.Where(x => x.AttachmentKey == attachmentKey && x.PkId == pkId));
                    //ctx.SaveChanges();

                    var attachment = new Attachments
                    {
                        PkId = pkId,
                        AttachmentCategory = attachmentCategory,
                        AttachmentKey = attachmentKey,
                        FileName = fileName,
                        Attachment = file,
                        ContentType = contentType,
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
            catch (Exception ex)
            {

            }
        }
        public static byte[] GetTimelinePicture(string attachmentKey, int pkId)
        {
            return db.Attachments.FirstOrDefault(x => x.AttachmentKey == attachmentKey && x.PkId == pkId)?.Attachment1;
        }
        public static List<AttachmentsModel> GetFilesList(string attachmentKey, int pkId)
        {
            var list = new List<AttachmentsModel>();
            var attachments = db.Attachments.Where(x => x.AttachmentKey == attachmentKey && x.PkId == pkId).ToList();
            foreach (var attachment in attachments)
            {
                var model = new AttachmentsModel { FileName = attachment.FileName, AttachmentId = attachment.AttachmentId, AttachmentCategory = attachment.AttachmentCategory };
                list.Add(model);
            }

            return list;
        }
    }
}