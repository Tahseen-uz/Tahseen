using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Books.Author;

public class AuthorForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set;}
    public string Biography { get; set; }
    public string AuthorImage { get; set; }
    public string Nationality { get; set; }
}