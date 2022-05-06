using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileUpload.Model
{
    public class AttachementDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150)]
        public string FromMail { get; set; }
        [StringLength(150)]
        public string ToMail { get; set; }
        public DateTime SendDate { get; set; }
    }
}
