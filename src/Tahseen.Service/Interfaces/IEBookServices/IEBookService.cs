using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.EBooks.EBook;

namespace Tahseen.Service.Interfaces.IEBookServices;

public interface IEBookService
{
    public Task<EBookForResultDto> AddAsync(EBookForCreationDto dto);
    public Task<EBookForResultDto> ModifyAsync(long id, EBookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<EBookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<EBookForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
