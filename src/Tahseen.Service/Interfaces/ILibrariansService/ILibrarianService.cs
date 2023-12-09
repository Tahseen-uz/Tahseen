using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Librarians;

namespace Tahseen.Service.Interfaces.ILibrariansService;

public interface ILibrarianService
{
    public Task<LibrarianForResultDto> AddAsync(LibrarianForCreationDto dto);
    public Task<LibrarianForResultDto> ModifyAsync(long id, LibrarianForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<LibrarianForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<LibrarianForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
