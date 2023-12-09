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
    private readonly IRepository<UserCart> userCartRepository;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<User> _userRepository;
    public WishlistService(
        IMapper mapper, 
        IRepository<WishList> repository,
        IRepository<UserCart> userCartRepository,
        IRepository<Book> bookRepository,
        IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userCartRepository = userCartRepository;
        this.bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<WishlistForResultDto> AddAsync(WishlistForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var book = await bookRepository
            .SelectAll()
            .Where(b => b.Id == dto.BookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (book == null || book.IsDeleted)
            throw new TahseenException(404, "Book not found");
        //shu joyini korib chiqamiz
        var cart = await userCartRepository.SelectAll().Where(u => u.UserId == dto.UserId && u.IsDeleted == false).FirstOrDefaultAsync();
        if(cart == null)
        {
            throw new TahseenException(404, "cart is not found");
        }
        var wishlist = new WishList
        {
            UserCartId = cart.Id,
            BookId = dto.BookId,
            Status = dto.Status
        };

        var insertedWishlist = await repository.CreateAsync(wishlist);

        return mapper.Map<WishlistForResultDto>(insertedWishlist);
    }

    public async Task<WishlistForResultDto> ModifyAsync(long id, WishlistForUpdateDto dto)
    {
        var wishlist = await this.repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (wishlist == null)
            throw new TahseenException(404, "wishlist not found");
        var cart = await userCartRepository.SelectAll().Where(u => u.UserId == dto.UserId && u.IsDeleted == false).FirstOrDefaultAsync();
        if (cart == null)
        {
            throw new TahseenException(404, "cart is not found");
        }

        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var book = await bookRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.BookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (book is null)
            throw new TahseenException(404, "Book is not found");

        wishlist.UserCartId = cart.Id;
        wishlist.BookId = dto.BookId;
        wishlist.Status = dto.Status;
        wishlist.UpdatedAt = DateTime.UtcNow;

        return mapper.Map<WishlistForResultDto>(await repository.UpdateAsync(wishlist));
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var wishlist = await repository
            .SelectAll()
            .Where(w => w.Id == id && !w.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (wishlist == null)
            throw new TahseenException(404, "Wishlist not found");

        return await repository.DeleteAsync(wishlist.Id);
    }

    public async Task<IEnumerable<WishlistForResultDto>> RetrieveAllAsync()
    {
        var wishlists = await this.repository.SelectAll()
            .Where(w => !w.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        var result = this.mapper.Map<IEnumerable<WishlistForResultDto>>(wishlists);
        foreach(var wishList in result)
        {
            wishList.Status = wishList.Status.ToString();
        }
        return result;
    }

    public async Task<WishlistForResultDto> RetrieveByIdAsync(long id)
    {
        var wishlist = await repository.SelectByIdAsync(id);
        if (wishlist is null || wishlist.IsDeleted)
            throw new TahseenException(404, "Wishlist not found");

        var result =  this.mapper.Map<WishlistForResultDto>(wishlist);
        result.Status = result.Status.ToString();
        return result;  
    }
}
