using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stack.DBModels;
using Stack.Models;
using Stack.Helpers;
using System.Data.Entity.Migrations;
namespace Stack.Areas.Admin.Models
{
    public class AdminQuestionsModel : AdminMenuItems
    {
        public int QuestionId { get; set; }
        public int QuestionCategoryId { get; set; }
        public string QuestionCategory { get; set; }
        public string QuestionCategoryUrl { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        //public List<QuestionCategories> GetQuestionCategories()
        //{
        //    using (var ctx = new StackContext())
        //    {
        //        return ctx.QuestionCategories.ToList();
        //    }
        //}
        public List<Questions> GetRecentQuestions()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Questions.ToList();
            }
        }
        public void Edit(int? id)
        {
            using (var ctx = new StackContext())
            {
                var question = ctx.Questions.ToList().Find(x => x.QuestionId == Convert.ToInt32(id));
                QuestionId = question.QuestionId;
                Question = question.Question;
                Description = question.Description;
                ShortDescription = question.ShortDescription;
            }
        }
        public void Delete(int? id)
        {
            using (var ctx = new StackContext())
            {
                var Question = ctx.Questions.ToList().Find(x => x.QuestionId == Convert.ToInt32(id));
                ctx.Questions.Remove(Question);
                ctx.SaveChanges();
            }
        }
        public void Save()
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var question = new Questions
                {
                    QuestionId = QuestionId,
                    Question = Question,
                    ShortDescription = ShortDescription,
                    Description = Description,
                    QuestionSeo = Helper.ToSeoFriendly(Question),
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    CreatedById = userDetails.UserId,
                    //CreatedBy = userDetails.FullName,
                    LastEditedById = userDetails.UserId
                };
                ctx.Questions.AddOrUpdate(question);
                ctx.SaveChanges();
            }
        }
    }
}