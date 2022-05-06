using FileUpload.Model;
using FileUpload.Services.FileUpload;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.Components
{
    public class ListViewComponent : ViewComponent
    {
        private readonly IFileUploadSystem fileUploadSystem;
        public ListViewComponent(IFileUploadSystem fileUploadSystem)
        {
            this.fileUploadSystem = fileUploadSystem;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await Task.Run(() => fileUploadSystem.List());
            return View(list);
        }
    }
}
