using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Service.Services.Books;

public class BookReviewService : IBookReviewService
{
    private readonly IMapper _mapper;
    private readonly IRepository<BookReviews> _repository;
    public BookReviewService(IMapper mapper, IRepository<BookReviews> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<BookReviewForResultDto> AddAsync(BookReviewForCreationDto dto)
    {
        var mapped = this._mapper.Map<BookReviews> (dto);
        var result = await this._repository.CreateAsync(mapped);
        return this._mapper.Map<BookReviewForResultDto>(result);    
    }

    public async Task<BookReviewForResultDto> RetrieveByIdAsync(long id)
    {
        var bookReview = await this._repository.SelectByIdAsync(id);
        if (bookReview == null || bookReview.IsDeleted)
            throw new TahseenException(404, "BookReview is not found");

        return this._mapper.Map<BookReviewForResultDto>(bookReview);
    }

    public async Task<BookReviewForResultDto> ModifyAsync(long id, BookReviewForUpdateDto dto)
    {
        var bookReview = await this._repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (bookReview == null)
            throw new TahseenException(404, "bookReviev doesn't found");

        var mapped = this._mapper.Map(dto, bookReview);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await this._repository.UpdateAsync(mapped);
        return this._mapper.Map<BookReviewForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    => await this._repository.DeleteAsync(id);

    public async Task<IEnumerable<BookReviewForResultDto>> RetrieveAllAsync()
    {
        var booksReview =  this._repository.SelectAll().Where(t=>!t.IsDeleted);
        
        return this._mapper.Map<IEnumerable<BookReviewForResultDto>>(booksReview);
    }
}
