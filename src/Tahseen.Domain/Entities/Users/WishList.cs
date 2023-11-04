using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Users;

public class WishList : Auditable
{
    public long UserCartId { get; set; }
    public UserCart Cart { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
    public WishListStatus Status { get; set; }
}