using Tahseen.Domain.Entities.Users;

namespace Tahseen.Service.DTOs.Users.UserCart
{
    public class UserCartForUpdateDto
    {
        public long UserId { get; set; }
        public IEnumerable<WishList> WishList { get; set; }
    }
}
