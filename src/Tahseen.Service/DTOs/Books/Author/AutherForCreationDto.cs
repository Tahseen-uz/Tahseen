using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Books.Author;

public class AuthorForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set;}
    public string Biography { get; set; }
    public IFormFile AuthorImage { get; set; }
    public Nationality Nationality { get; set; }
}