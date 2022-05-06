using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FileUpload.Model
{
    public class UploadFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] File { get; set; }
        [StringLength(50)]
        public string Extension { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
