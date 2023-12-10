using Newtonsoft.Json;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Users.BorrowedBook
{
    public class BorrowedBookForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public string BookTitle { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
        public decimal FineAmount { get; set; }
    }
}
