using Newtonsoft.Json;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Users.BorrowedBook
{
    public class BorrowedBookForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserForResultDto User { get; set; }
        public long BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowedBookStatus Status { get; set; }
        public decimal FineAmount { get; set; }
        public BookForResultDto Book { get; set; }
    }
}
