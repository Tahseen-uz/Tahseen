using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Librarians;

public class LibrarianForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public IFormFile Image { get; set; }
}
