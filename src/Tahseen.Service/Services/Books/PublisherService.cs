using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Service.Services.Books;

public class PublisherService : IPublisherService
{
    private readonly IMapper mapper;
    private readonly IRepository<Publisher> repository;
    public PublisherService(IMapper mapper, IRepository<Publisher> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto)
    {
        var mapped = mapper.Map<Publisher>(dto);
        var result = await this.repository.CreateAsync(mapped);

        return mapper.Map<PublisherForResultDto>(result);
    }

    public async ValueTask<PublisherForResultDto> RetrieveByIdAsync(long id)
    {
        var publisher = await this.repository.SelectByIdAsync(id);
        if (publisher == null || publisher.IsDeleted)
            throw new TahseenException(404, "Publisher doesn't found");
        
        return mapper.Map<PublisherForResultDto>(publisher);
    }

    public async Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto)
    {
        var publisher = await this.repository.SelectByIdAsync(id);
        if (publisher == null || publisher.IsDeleted)
            throw new TahseenException(404, "Publisher doesn't found");

        var publisherMapped = mapper.Map(dto, publisher);
        var result = await this.repository.UpdateAsync(publisherMapped);

        return mapper.Map<PublisherForResultDto>(result);
    }

    public Task<bool> RemoveAsync(long id)
    => this.repository.DeleteAsync(id);

    public IQueryable<PublisherForResultDto> RetrieveAll()
    {
        var results = this.repository.SelectAll().Where(t => !t.IsDeleted);
        return mapper.Map<IQueryable<PublisherForResultDto>>(results);
    }
}
