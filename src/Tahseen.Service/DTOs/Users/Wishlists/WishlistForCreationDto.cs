using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.Wishlists;
public class WishlistForCreationDto
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public long CartId { get; set; }
    public WishListStatus Status { get; set; }
}
