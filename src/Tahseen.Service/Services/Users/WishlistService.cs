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
    public WishlistService(IMapper mapper, IRepository<WishList> repository,
        IRepository<UserCart> userCartRepository,
        IRepository<Book> bookRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userCartRepository = userCartRepository;
        this.bookRepository = bookRepository;
    }

    public async Task<WishlistForResultDto> AddAsync(WishlistForCreationDto dto)
    {
        var book = await bookRepository.SelectAll().Where(b => b.Id == dto.BookId).FirstOrDefaultAsync();
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
        wishlist.UserCartId = cart.Id;
        wishlist.BookId = dto.BookId;
        wishlist.Status = dto.Status;
        wishlist.UpdatedAt = DateTime.UtcNow;

        return mapper.Map<WishlistForResultDto>(await repository.UpdateAsync(wishlist));
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var wishlist = await repository.SelectByIdAsync(id);
        if (wishlist == null || wishlist.IsDeleted)
            throw new TahseenException(404, "Wishlist not found");

        return await repository.DeleteAsync(wishlist.Id);
    }

    public async Task<IEnumerable<WishlistForResultDto>> RetrieveAllAsync()
    {
        var wishlists = repository.SelectAll().Where(w => !w.IsDeleted);
        return mapper.Map<IEnumerable<WishlistForResultDto>>(wishlists);
    }

    public async Task<WishlistForResultDto> RetrieveByIdAsync(long id)
    {
        var wishlist = await repository.SelectByIdAsync(id);
        if (wishlist is null || wishlist.IsDeleted)
            throw new TahseenException(404, "Wishlist not found");

        return mapper.Map<WishlistForResultDto>(wishlist);
    }
}
