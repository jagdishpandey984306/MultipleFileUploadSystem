using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileUpload.Model
{
    public class FileUploadDetails
    {
        public string ToEmail { get; set; }
        public List<IFormFile> files { get; set; }

    }
}
