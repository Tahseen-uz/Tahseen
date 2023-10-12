using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Books.CompletedBooks;

public class CompletedBookForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public long BookId { get; set; }
    //public BookForResultDto Book { get; set; }
    public string Comment { get; set; }
}
