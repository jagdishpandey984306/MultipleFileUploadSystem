namespace FileUpload.Model
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public byte[] File { get; set; }
    }
}
