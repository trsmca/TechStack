//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stack.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuItem1 { get; set; }
        public string MenuItemSeo { get; set; }
        public string MenuUrl { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastEditedById { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
    }
}