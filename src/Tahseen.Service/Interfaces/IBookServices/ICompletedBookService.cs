using Tahseen.Service.DTOs.Books.CompletedBooks;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface ICompletedBookService
{
    public Task<CompletedBookForResultDto> AddAsync(CompletedBookForCreationDto dto);
    public Task<CompletedBookForResultDto> ModifyAsync(long id, CompletedBookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<CompletedBookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<CompletedBookForResultDto>> RetrieveAllAsync();
}
