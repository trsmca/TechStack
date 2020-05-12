using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Stack.DBModels;
using Stack.Helpers;
using Stack.Models;

namespace Stack.Areas.Admin.Models
{
    public class AdminMenuItems
    {
        public int MenuItemId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuUrl { get; set; }
        public List<MenuItems> GetMenuItems()
        {
            using (var ctx = new StackContext())
            {
                return ctx.MenuItems.ToList();
            }
        }
        public static List<MenuItems> GetArticleCategories()
        {
            using (var ctx = new StackContext())
            {
                return ctx.MenuItems.Where(x => x.ParentMenuId == 2).ToList();
            }
        }
        public void Save()
        {
            using (var db = new StackContext())
            {
                var model = new MenuItems
                {
                    ParentMenuId = ParentMenuId,
                    MenuItemId = MenuItemId,
                    MenuItem = MenuItemName,
                    MenuItemSeo = Helper.ToSeoFriendly(MenuItemName),
                    MenuUrl = MenuUrl,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now,
                };
                db.MenuItems.AddOrUpdate(model);
                db.SaveChanges();
            }
        }
        public void Edit(int? id)
        {
            using (var ctx = new StackContext())
            {
                var menu = ctx.MenuItems.ToList().Find(x => x.MenuItemId == Convert.ToInt32(id));
                MenuItemId = menu.MenuItemId;
                MenuItemName = menu.MenuItem;
                MenuUrl = menu.MenuUrl;
            }
        }
        public void Delete(int? id)
        {
            using (var ctx = new StackContext())
            {
                var menu = ctx.MenuItems.ToList().Find(x => x.MenuItemId == Convert.ToInt32(id));
                ctx.MenuItems.Remove(menu);
                ctx.SaveChanges();
            }
        }
    }
}