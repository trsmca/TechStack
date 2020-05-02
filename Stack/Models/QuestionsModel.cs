using System;
using System.Collections.Generic;
using System.Linq;
using Stack.DBModels;

namespace Stack.Models
{
    public class QuestionsModel : BaseModel
    {
        public int QuestionId { get; set; }
        public int QuestionCategoryId { get; set; }
        public string QuestionCategory { get; set; }
        public string QuestionCategoryUrl { get; set; }

        public string Question { get; set; }
        public string QuestionNameSeo { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ProfilePicUrl { get; set; }
        public string MenuItemSeo { get; set; }

        public List<Questions> QuestionsList { get; set; }
        public List<Questions> GetRecentQuestions()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Questions.Take(6).ToList();
            }
        }
        public List<Questions> GetQuestionsOnMenuItemId(int pageNumber)
        {
            using (var ctx = new StackContext())
            {
                int skipRecords = (pageNumber != 0 ? pageNumber - 1 : pageNumber) * 5;
                QuestionsList = ctx.Questions.ToList();
                TotalRecords = QuestionsList.Count;
                QuestionsList = QuestionsList.OrderBy(m => m.Question).Skip(skipRecords).Take(5).ToList();
                return QuestionsList;
            }
        }
        public void Edit(string id)
        {
            //var userDetails = UserDetails.Instance;
            using (var ctx = new StackContext())
            {
                var question = ctx.Questions.ToList().Find(x => x.QuestionSeo == id);
                QuestionId = question.QuestionId;
                Question= question.Question;
                QuestionNameSeo = question.QuestionSeo;
                Description = question.Description;
                //CreatedBy = question.CreatedBy;
                LastEditedDate = question.LastEditedDate;
                //ProfilePicUrl = userDetails.ProfilePicUrl;
                var accountModel = new AccountModel();
                var user = accountModel.GetUsers(question.CreatedById);
                ProfilePicUrl = user.ProfilePicUrl;
            }
        }
    }
}