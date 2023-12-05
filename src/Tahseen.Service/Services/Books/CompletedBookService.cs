using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.CompletedBooks;

namespace Tahseen.Service.Services.Books;

public class CompletedBookService : ICompletedBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<CompletedBooks> _repository;
    public CompletedBookService(IMapper mapper, IRepository<CompletedBooks> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<CompletedBookForResultDto> AddAsync(CompletedBookForCreationDto dto)
    {
        var mapped = this._mapper.Map<CompletedBooks>(dto);
        var result = await this._repository.CreateAsync(mapped);
        return this._mapper.Map<CompletedBookForResultDto>(result);
    }

    public async Task<CompletedBookForResultDto> ModifyAsync(long id, CompletedBookForUpdateDto dto)
    {
        var completedBook = await this._repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (completedBook == null)
            throw new TahseenException(404, "CompletedBook not found");

        var mapped = this._mapper.Map(dto, completedBook);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = this._repository.UpdateAsync(mapped);
        return this._mapper.Map<CompletedBookForResultDto>(result);
    }

    public Task<bool> RemoveAsync(long id)
    => this._repository.DeleteAsync(id);

    public async Task<IEnumerable<CompletedBookForResultDto>> RetrieveAllAsync()
    {
        var bookCompleted = await this._repository
            .SelectAll()
            .Where(t=>!t.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return this._mapper.Map<IEnumerable<CompletedBookForResultDto>>(bookCompleted);
    }

    public async Task<CompletedBookForResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this._repository
            .SelectAll()
            .Where(c => c.Id == id && !c.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (book is null)
            throw new TahseenException(404, "CompletedBook does not found");

        return this._mapper.Map<CompletedBookForResultDto>(book);
    }
}
