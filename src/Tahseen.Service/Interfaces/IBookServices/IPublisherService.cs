using Tahseen.Domain.Configurations;
using Tahseen.Service.DTOs.Books.Publishers;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IPublisherService
{
    Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto);
    Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    ValueTask<PublisherForResultDto?> RetrieveByIdAsync(long id);
    IQueryable<PublisherForResultDto> RetrieveAll();
}
