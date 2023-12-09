using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.DTOs.Feedbacks.Feedback;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Service.Services.Feedbacks;

public class FeedbackService:IFeedbackService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Feedback> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Book> _bookRepository;

    public FeedbackService(
        IMapper mapper, 
        IRepository<Feedback>repository,
        IRepository<User> userRepository,
        IRepository<Book> bookRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    public async Task<FeedbackForResultDto> AddAsync(FeedbackForCreationDto dto)
    {
        var user = await _userRepository.SelectAll().
           Where(u => u.Id == dto.UserId && u.IsDeleted == false).
           AsNoTracking().
           FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll().
            Where(e => e.Id == dto.BookId && e.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (book == null)
            throw new TahseenException(404, "Book is not found");

        var mapped = _mapper.Map<Feedback> (dto);
        var result = await _repository.CreateAsync(mapped);
        return _mapper.Map<FeedbackForResultDto>(result); 
    }

    public async Task<FeedbackForResultDto> ModifyAsync(long id, FeedbackForUpdateDto dto)
    {
        var bookReview = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (bookReview == null)
            throw new TahseenException(404, "Feedback doesn't found");

        var user = await _userRepository.SelectAll().
           Where(u => u.Id == dto.UserId && u.IsDeleted == false).
           AsNoTracking().
           FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll().
            Where(e => e.Id == dto.BookId && e.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (book == null)
            throw new TahseenException(404, "Book is not found");

        var mapped = _mapper.Map(dto, bookReview);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.UpdateAsync(mapped);
        return _mapper.Map<FeedbackForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
        => await _repository.DeleteAsync(id);

    public async Task<FeedbackForResultDto?> RetrieveByIdAsync(long id)
    {
        var bookReview = await _repository.SelectByIdAsync(id);
        if (bookReview == null || bookReview.IsDeleted)
            throw new TahseenException(404, "Feedback doesn't found");

        return _mapper.Map<FeedbackForResultDto>(bookReview);
    }

    public async Task<IEnumerable<FeedbackForResultDto>> RetrieveAllAsync()
    {
        var booksReview =  _repository.SelectAll().Where(x=>!x.IsDeleted);
        
        return _mapper.Map<IEnumerable<FeedbackForResultDto>>(booksReview);
    }
}