using Microsoft.AspNetCore.Http;

namespace FileUpload.Model
{
    public class AttachmentsFile: SenderModel
    {
        public byte[][] Attachments { get; set; }
        public string[] Extension { get; set; }
    }
}
