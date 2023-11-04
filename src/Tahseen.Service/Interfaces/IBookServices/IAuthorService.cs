using Tahseen.Service.DTOs.Books.Author;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IAuthorService
{
    public Task<AuthorForResultDto> AddAsync(AuthorForCreationDto dto);
    public Task<AuthorForResultDto> ModifyAsync(long id, AuthorForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<AuthorForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<AuthorForResultDto>> RetrieveAllAsync();
}