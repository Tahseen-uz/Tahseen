using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Publishers;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IPublisherService
{
    public Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto);
    public Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<PublisherForResultDto?> RetrieveByIdAsync(long id);
    public Task<IEnumerable<PublisherForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
