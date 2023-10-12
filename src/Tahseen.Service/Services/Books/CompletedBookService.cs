using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.CompletedBooks;

namespace Tahseen.Service.Services.Books;

public class CompletedBookService : ICompletedBookService
{
    private readonly IMapper mapper;
    private readonly IRepository<CompletedBooks> repository;
    public CompletedBookService(IMapper mapper, IRepository<CompletedBooks> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<CompletedBookForResultDto> AddAsync(CompletedBookForCreationDto dto)
    {
        var mapped = this.mapper.Map<CompletedBooks>(dto);
        var result = await this.repository.CreateAsync(mapped);
        return mapper.Map<CompletedBookForResultDto>(result);
    }

    public async Task<CompletedBookForResultDto> ModifyAsync(long id, CompletedBookForUpdateDto dto)
    {
        var completedBook = await this.repository.SelectByIdAsync(id);
        if (completedBook == null || completedBook.IsDeleted)
            throw new TahseenException(404, "CompletedBook not found");

        var mapped = this.mapper.Map(dto, completedBook);
        var result = this.repository.UpdateAsync(mapped);
        return this.mapper.Map<CompletedBookForResultDto>(result);
    }

    public Task<bool> RemoveAsync(long id)
    => this.repository.DeleteAsync(id);

    public IQueryable<CompletedBookForResultDto> RetrieveAll()
    {
        var bookCompleted = this.repository.SelectAll().Where(t=>!t.IsDeleted);

        return this.mapper.Map<IQueryable<CompletedBookForResultDto>>(bookCompleted);
    }

    public async ValueTask<CompletedBookForResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this.repository.SelectByIdAsync(id);
        if (book == null || book.IsDeleted)
            throw new TahseenException(404, "CompletedBook does not found");

        return this.mapper.Map<CompletedBookForResultDto>(book);
    }
}
