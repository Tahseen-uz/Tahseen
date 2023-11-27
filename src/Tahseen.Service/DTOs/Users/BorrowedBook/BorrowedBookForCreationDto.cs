using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.BorrowedBook
{
    public class BorrowedBookForCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
    }
}
