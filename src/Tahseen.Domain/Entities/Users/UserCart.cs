using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Users;

public class UserCart : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public IEnumerable<WishList> WishList { get; set; }
}
