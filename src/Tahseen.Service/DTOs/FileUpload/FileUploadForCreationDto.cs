using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.FileUpload
{
    public class FileUploadForCreationDto
    {
        public string FolderPath { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
