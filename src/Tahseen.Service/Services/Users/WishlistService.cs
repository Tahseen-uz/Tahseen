using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Data.Repositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Users;
using Tahseen.Service.DTOs.Users.Wishlists;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users;

public class WishlistService : IWishlistService
{
    private readonly IMapper mapper;
    private readonly IRepository<WishList> repository;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<User> _userRepository;
    public WishlistService(
        IMapper mapper, 
        IRepository<WishList> repository,
        IRepository<Book> bookRepository,
        IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.bookRepository = bookRepository;
        this._userRepository = userRepository;
    }

    public async Task<WishlistForResultDto> AddAsync(long UserId, WishlistForCreationDto dto)
    {
        var checkAvailibility = await repository.SelectAll().Where(e => e.UserId == UserId && e.BookId == dto.BookId && e.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync();
        if(checkAvailibility != null)
        {
            throw new TahseenException(400, "This book is available in your cart");
        }
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user == null || user.IsDeleted == true)
            throw new TahseenException(404, "User is not found");

        var book = await bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (book == null || book.IsDeleted == true)
            throw new TahseenException(404, "Book not found");
        var wishlist = new WishList
        {
            BookId = dto.BookId,
            UserId = UserId
        };

        var insertedWishlist = await repository.CreateAsync(wishlist);

        return mapper.Map<WishlistForResultDto>(insertedWishlist);
    }


    //No need
    public async Task<WishlistForResultDto> ModifyAsync(long id, WishlistForUpdateDto dto)
    {
        var wishlist = await this.repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync();
        if (wishlist == null || wishlist.IsDeleted == true)
            throw new TahseenException(404, "wishlist not found");
 
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user == null || user.IsDeleted == true)
            throw new TahseenException(404, "User is not found");

        var book = await bookRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.BookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (book == null || user.IsDeleted == true)
            throw new TahseenException(404, "Book is not found");

        wishlist.BookId = dto.BookId;
        wishlist.UpdatedAt = DateTime.UtcNow;
        var res = await repository.UpdateAsync(wishlist);
        return mapper.Map<WishlistForResultDto>(await repository.UpdateAsync(res));
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var wishlist = await repository
            .SelectAll()
            .Where(w => w.Id == id && w.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (wishlist == null)
            throw new TahseenException(404, "Wishlist not found");

        return await repository.DeleteAsync(wishlist.Id);
    }

    public async Task<IEnumerable<WishlistForResultDto>> RetrieveAllAsync(long UserId) // UserId
    {
        var wishlists = await this.repository.SelectAll()
            .Where(e => e.UserId == UserId && e.IsDeleted == false)
            .Include(e => e.Book)
            .ThenInclude(e => e.LibraryBranch)
            .AsNoTracking()
            .ToListAsync();
        return this.mapper.Map<IEnumerable<WishlistForResultDto>>(wishlists); 
    }

    public async Task<WishlistForResultDto> RetrieveByIdAsync(long id) //UserId
    {
        var wishlist = await repository.SelectByIdAsync(id);
        if (wishlist is null || wishlist.IsDeleted)
            throw new TahseenException(404, "Wishlist not found");

        var result =  this.mapper.Map<WishlistForResultDto>(wishlist);
        return result;  
    }
}
