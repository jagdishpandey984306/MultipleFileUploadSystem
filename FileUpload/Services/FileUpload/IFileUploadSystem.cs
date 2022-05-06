using FileUpload.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.Services.FileUpload
{
    public interface IFileUploadSystem
    {
        Task<ResponseMessage> UploadFile(FileUploadDetails param);
        Task<List<UploadFile>> List();
        Task<ResponseMessage> Delete(int id);
    }
}
