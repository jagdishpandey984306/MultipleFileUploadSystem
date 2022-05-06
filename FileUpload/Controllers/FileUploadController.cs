using FileUpload.Model;
using FileUpload.Services.FileUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.Controllers
{

    public class FileUploadController : Controller
    {
        private readonly IFileUploadSystem fileUploadSystem;
        public FileUploadController(IFileUploadSystem fileUploadSystem)
        {
            this.fileUploadSystem = fileUploadSystem;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadDetails model)
        {
            var result = await fileUploadSystem.UploadFile(model);
            return Json(result);
        }

        public async Task<IActionResult> List()
        {
            var result = await fileUploadSystem.List();
            return ViewComponent("List", result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await fileUploadSystem.Delete(id);
            return Json(result);
        }
    }
}
