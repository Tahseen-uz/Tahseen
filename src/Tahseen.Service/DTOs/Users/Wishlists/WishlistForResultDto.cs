using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Users.Wishlists;

public class WishlistForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public long UserCartId { get; set; }
    public long BookId { get; set; }
    public BookForResultDto Book { get; set; }
    public WishListStatus Status { get; set; }
}
