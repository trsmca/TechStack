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
    
    public partial class Attachment
    {
        public int AttachmentId { get; set; }
        public int PkId { get; set; }
        public string AttachmentCategory { get; set; }
        public string AttachmentKey { get; set; }
        public string FileName { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastEditedById { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
        public byte[] Attachment1 { get; set; }
    }
}