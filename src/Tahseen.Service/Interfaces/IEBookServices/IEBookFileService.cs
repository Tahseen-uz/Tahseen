using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.EBooks.EBook;
using Tahseen.Service.DTOs.EBooks.EBookFile;

namespace Tahseen.Service.Interfaces.IEBookServices;

public interface IEBookFileService
{
    public Task<EBookFileForResultDto> AddAsync(EBookFileForCreationDto dto);
    public Task<EBookFileForResultDto> ModifyAsync(long id, EBookFileForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<EBookFileForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<EBookFileForResultDto>> RetrieveAllAsync();
}
