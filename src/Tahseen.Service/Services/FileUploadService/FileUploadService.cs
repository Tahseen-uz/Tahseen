using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<bool> FileDeleteAsync(string filePath)
        {
            var fullPath = Path.Combine(WebEnvironmentHost.WebRootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }


        public async Task<FileUploadForResultDto> FileUploadAsync(FileUploadForCreationDto dto)
        {
            if (dto.FormFile == null)
            {
                throw new TahseenException(400, "Image is null");
            }
            var WwwRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", $"{dto.FolderPath}");
            var assetsFolderPath = Path.Combine(WwwRootPath, "Assets");
            var ImagesFolderPath = Path.Combine(assetsFolderPath, $"{dto.FolderPath}");

            if (!Directory.Exists(assetsFolderPath))
            {
                Directory.CreateDirectory(assetsFolderPath);
            }

            if (!Directory.Exists(ImagesFolderPath))
            {
                Directory.CreateDirectory(ImagesFolderPath);
            }
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.FormFile.FileName);
            var fullPath = Path.Combine(WwwRootPath, fileName);

            using (var streamFile = File.OpenWrite(fullPath))
            {
                await dto.FormFile.CopyToAsync(streamFile);
            }

            var result = new FileUploadForResultDto()
            {
                FileName = fileName,
                FolderPath = dto.FolderPath
            };

            return result;
        }
    }
}
