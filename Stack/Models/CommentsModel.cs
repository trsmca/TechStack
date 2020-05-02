using System;
using System.Collections.Generic;
using System.Linq;
using Stack.DBModels;
using System.Data.Entity.Migrations;
using Stack.Helpers;

namespace Stack.Models
{
    public class CommentsModel : BaseModel
    {
        public int CommentId { get; set; }
        public int PkId { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public int ParentId { get; set; }
        public string Pings { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string CreatedByAdmin { get; set; }
        public int UpvoteCount { get; set; }
        public bool UserHasUpvoted { get; set; }

        public void Save()
        {
            var userDetails = UserDetails.Instance;
            using (var ctx = new StackContext())
            {
                var comment = new Comments
                {
                    CommentId = CommentId,
                    PkId = PkId,
                    Category = Category,
                    Comment = Comment,
                    ParentId = ParentId,
                    UpvoteCount = UpvoteCount,
                    UserHasUpvoted = UserHasUpvoted,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    CreatedById = userDetails.UserId,
                    CreatedBy = userDetails.FullName,
                    LastEditedById = userDetails.UserId,
                    UserId = userDetails.UserId
                };
                ctx.Comments.AddOrUpdate(comment);
                ctx.SaveChanges();
            }
        }
        public string GetComments(int pkid, string category)
        {
            var userDetails = UserDetails.Instance;
            using (var ctx = new StackContext())
            {
                var comments = ctx.Comments.Where(m => m.PkId == pkid && m.Category == category).ToList();
                var list = new List<CommentsJsonDataModel>();
                foreach (var item in comments)
                {
                    CommentsJsonDataModel model = new CommentsJsonDataModel();
                    model.id = item.CommentId;
                    model.parent = item.ParentId == 0 ? null : item.ParentId.ToString();
                    model.created = item.CreatedDate.ToString();
                    model.modified = item.LastEditedDate.ToString();
                    model.content = item.Comment;
                    model.creator = item.CreatedById;
                    model.fullname = item.CreatedBy;
                    model.created_by_admin = false;
                    if (userDetails.UserId == item.CreatedById)
                        model.created_by_current_user = true;
                    else
                        model.created_by_current_user = false;
                    model.upvote_count = item.UpvoteCount;
                    model.user_has_upvoted = item.UserHasUpvoted;
                    var accountModel=new AccountModel();
                    var user = accountModel.GetUsers(item.CreatedById);
                    model.profile_picture_url = user.ProfilePicUrl;
                    list.Add(model);
                }
                return list.ToJSON();
            }
        }
    }
}