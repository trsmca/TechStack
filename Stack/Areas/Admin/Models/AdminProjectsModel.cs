using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stack.DBModels;
using Stack.Models;
using Stack.Helpers;
using System.Data.Entity.Migrations;
using System.Data.Entity;
namespace Stack.Areas.Admin.Models
{
    public class AdminProjectsModel : AdminMenuItems
    {
        public int ProjectId { get; set; }
        public int ProjectCategoryId { get; set; }
        public string ProjectCategory { get; set; }
        public string ProjectCategoryUrl { get; set; }

        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public HttpPostedFileBase[] CoverPhoto { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        //public List<ProjectCategories> GetProjectCategories()
        //{
        //    using (var ctx = new StackContext())
        //    {
        //        return ctx.ProjectCategories.ToList();
        //    }
        //}
        public List<Projects> GetRecentProjects()
        {
            using (var ctx = new StackContext())
            {
                return ctx.Projects.ToList();
            }
        }
        public void Edit(int? id)
        {
            using (var ctx = new StackContext())
            {
                var Project = ctx.Projects.ToList().Find(x => x.ProjectId == Convert.ToInt32(id));
                ProjectId = Project.ProjectId;
                ProjectName = Project.ProjectName;
                Description = Project.Description;
                ShortDescription = ShortDescription;
            }
        }
        public void Delete(int? id)
        {
            using (var ctx = new StackContext())
            {
                var Project = ctx.Projects.ToList().Find(x => x.ProjectId == Convert.ToInt32(id));
                ctx.Projects.Remove(Project);
                ctx.SaveChanges();
            }
        }
        public void Save()
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var project = new Projects
                {
                    ProjectId = ProjectId,
                    MenuItemId = MenuItemId,
                    ProjectName = ProjectName,
                    ShortDescription = ShortDescription,
                    Description = Description,
                    ProjectNameSeo = Helper.ToSeoFriendly(ProjectName),
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                    CreatedById = userDetails.UserId,
                    //CreatedBy = userDetails.FullName,
                    LastEditedById = userDetails.UserId
                };
                ctx.Entry(project).State = EntityState.Modified;
                ctx.Projects.AddOrUpdate(project);
                ctx.SaveChanges();
                ProjectId = project.ProjectId;
            }
        }
        public void SaveAttachments(string fileName, string AttachmentKey)
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var attachment = new Attachments
                {
                    PkId = ProjectId,
                    AttachmentCategory = "Projects",
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
        public int SaveProjectFiles(int projectFileId, int projectId, int parentId, string fileName)
        {
            var userDetails = UserDetails.Instance;

            using (var ctx = new StackContext())
            {
                var projectFile = new ProjectFiles
                {
                    ProjectFileId = projectFileId,
                    ProjectId = projectId,
                    ParentId = parentId,
                    FileName = fileName
                };
                //ctx.Entry(Project).State = EntityState.Modified;
                ctx.ProjectFiles.AddOrUpdate(projectFile);
                ctx.SaveChanges();
                return projectFile.ProjectFileId;
            }
        }
    }
}