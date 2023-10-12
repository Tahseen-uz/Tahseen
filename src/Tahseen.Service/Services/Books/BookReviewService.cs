using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Service.Services.Books;

public class BookReviewService : IBookReviewService
{
    private readonly IMapper mapper;
    private readonly IRepository<BookReviews> repository;
    public BookReviewService(IMapper mapper, IRepository<BookReviews> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<BookReviewForResultDto> AddAsync(BookReviewForCreationDto dto)
    {
        var mapped = this.mapper.Map<BookReviews> (dto);
        var result = await this.repository.CreateAsync(mapped);
        return this.mapper.Map<BookReviewForResultDto>(result);    
    }

    public async ValueTask<BookReviewForResultDto> RetrieveByIdAsync(long id)
    {
        var bookReview = await this.repository.SelectByIdAsync(id);
        if (bookReview == null || bookReview.IsDeleted)
            throw new TahseenException(404, "BookReviev doesn't found");

        return this.mapper.Map<BookReviewForResultDto>(bookReview);
    }

    public async Task<BookReviewForResultDto> ModifyAsync(long id, BookReviewForUpdateDto dto)
    {
        var bookReview = await this.repository.SelectByIdAsync (id);
        if (bookReview == null || bookReview.IsDeleted)
            throw new TahseenException(404, "bookReviev doesn't found");

        var mapped = this.mapper.Map(dto, bookReview);

        var result = await this.repository.UpdateAsync(mapped);
        return this.mapper.Map<BookReviewForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    => await this.repository.DeleteAsync(id);

    public IQueryable<BookReviewForResultDto> RetrieveAll()
    {
        var booksReview =  this.repository.SelectAll().Where(t=>!t.IsDeleted);
        
        return this.mapper.Map<IQueryable<BookReviewForResultDto>>(booksReview);
    }
}
