using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Stack.DBModels;
using Stack.Models;

namespace Stack.Models
{
    public class MenuItemsModel
    {
        public int MenuItemId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuItem { get; set; }
        public string MenuUrl { get; set; }
        public static List<MenuItems> GetParentMenuItems()
        {
            using (var ctx = new StackContext())
            {
                return ctx.MenuItems.Where(m => m.ParentMenuId == 0).ToList();
            }
        }
        public static List<MenuItems> GetMenuItemsOnParentId(int parentId)
        {
            using (var ctx = new StackContext())
            {
                return ctx.MenuItems.Where(m => m.ParentMenuId == parentId).ToList();
            }
        }
    }
}