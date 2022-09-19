// LightningBits
using System;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerce_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UpLoadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}

