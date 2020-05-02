using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Stack.DBModels
{
    public class Attachments : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentId { get; set; }
        public int PkId { get; set; }
        public string AttachmentCategory { get; set; }
        public string AttachmentKey { get; set; }
        public byte[] Attachment { get; set; }
        public string FileName { get; set; }
    }
}