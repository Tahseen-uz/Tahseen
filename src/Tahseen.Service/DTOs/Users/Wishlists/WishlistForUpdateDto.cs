using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.Wishlists;
public class WishlistForUpdateDto
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public WishListStatus Status { get; set; }
}
