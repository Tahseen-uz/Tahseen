using Tahseen.Service.DTOs.FileUpload;

namespace Tahseen.Service.Interfaces.IFileUploadService;

public interface IFileUploadService
{
    public Task<FileUploadForResultDto> FileUploadAsync(FileUploadForCreationDto dto);
    public Task<bool> FileDeleteAsync(string filePath);
}
