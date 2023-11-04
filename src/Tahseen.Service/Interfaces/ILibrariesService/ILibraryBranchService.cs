using Tahseen.Service.DTOs.Libraries.LibraryBranch;

namespace Tahseen.Service.Interfaces.ILibrariesService;

public interface ILibraryBranchService
{
    public Task<LibraryBranchForResultDto> AddAsync(LibraryBranchForCreationDto dto);
    public Task<LibraryBranchForResultDto> ModifyAsync(long id, LibraryBranchForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<LibraryBranchForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<LibraryBranchForResultDto>> RetrieveAllAsync();
}
