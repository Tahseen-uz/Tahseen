using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Libraries.LibraryBranch;

public class LibraryBranchForUpdateDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public IFormFile Image { get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; }
    public LibraryType LibraryType { get; set; }

}
