using System;
using System.Collections.Generic;
using System.Linq;
using Stack.DBModels;

namespace Stack.Models
{
    public class ProjectsModel : BaseModel
    {
        public int ProjectId { get; set; }
        public int ProjectCategoryId { get; set; }
        public string ProjectCategory { get; set; }
        public string ProjectCategoryUrl { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNameSeo { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ProfilePicUrl { get; set; }
        public string MenuItemSeo { get; set; }

        public string AttachmentNames { get; set; }

        public List<ProjectFiles> ProjectFiles { get; set; }

        public List<Projects> ProjectsList { get; set; }
        public List<Projects> GetRecentProjects()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Projects.OrderByDescending(x => x.ProjectId).Take(6).ToList();
            }
        }
        public List<Projects> GetProjectsOnMenuItemId(string menuItemId, int pageNumber)
        {
            using (var ctx = new StackContext())
            {
                int skipRecords = (pageNumber != 0 ? pageNumber - 1 : pageNumber) * 5;
                ProjectsList = ctx.Projects.Where(m => m.MenuItems.MenuItemSeo == menuItemId).ToList();
                TotalRecords = ProjectsList.Count;
                ProjectsList = ProjectsList.OrderBy(m => m.ProjectName).Skip(skipRecords).Take(5).ToList();
                return ProjectsList;
            }
        }
        public void Edit(string id)
        {
            //var userDetails = UserDetails.Instance;
            using (var ctx = new StackContext())
            {
                var Project = ctx.Projects.ToList().Find(x => x.ProjectNameSeo == id);
                ProjectId = Project.ProjectId;
                ProjectName = Project.ProjectName;
                ProjectNameSeo = Project.ProjectNameSeo;
                Description = Project.Description;
                //CreatedBy = Project.CreatedBy;
                LastEditedDate = Project.LastEditedDate;
                var attachments = ctx.Attachments.ToList().Where(x => x.PkId == ProjectId && x.AttachmentCategory == "Projects" && x.AttachmentKey == "Abstratct").ToList();
                //AttachmentNames =
                foreach (var item in attachments)
                {
                    if (AttachmentNames != null && AttachmentNames != string.Empty)
                        AttachmentNames = AttachmentNames + "||" + item.FileName;
                    else
                        AttachmentNames = item.FileName;
                }
                //ProfilePicUrl = userDetails.ProfilePicUrl;
                var accountModel = new AccountModel();
                accountModel.GetUsers(Project.CreatedById);
                CreatedBy = accountModel.FirstName + " " + accountModel.LastName;
                ProfilePicUrl = accountModel.ProfilePicUrl;
                ProjectFiles = ctx.ProjectFiles.ToList();
            }
        }
    }
}