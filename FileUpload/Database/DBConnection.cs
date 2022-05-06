

using Microsoft.EntityFrameworkCore;
using FileUpload.Model;

namespace FileUpload.Database
{
    public class DBConnection : DbContext
    {
        public DBConnection()
        {
        }

        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }

        public virtual DbSet<UploadFile> UploadFile { get; set; }
        public virtual DbSet<AttachementDetails> AttachementDetails { get; set; }
    }
}
