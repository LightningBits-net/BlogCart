// LightningBits
using System;
using Azure.Core;
using ECommerce_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerce_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
     

        public bool DeleteFile(string filePath)
        {
            if(File.Exists(_webHostEnvironment.WebRootPath+filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }
            return false;
        }

        public async Task<string> UpLoadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString()+fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}/img/product";
            if(!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var filePath = Path.Combine(folderDirectory, fileName);

            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream(long.MaxValue).CopyToAsync(fs);

            var fullPath = $"/img/product/{fileName}";
            return fullPath;
        }

    //        // Get the collection of files from the request
    //        HttpFileCollection files = Request.Files;

    //// Loop through the collection and save each file to the server
    //for (int i = 0; i<files.Count; i++)
    //{
    //    HttpPostedFile file = files[i];

    //        // Save the file to the server
    //        file.SaveAs(Server.MapPath("~/uploads/" + file.FileName));
    //}
}
}

