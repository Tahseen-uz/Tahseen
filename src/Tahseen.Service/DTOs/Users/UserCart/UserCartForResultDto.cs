using Tahseen.Domain.Entities.Users;

namespace Tahseen.Service.DTOs.Users.UserCart
{
    public class UserCartForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public IEnumerable<WishList> WishList { get; set; }
    }
}
