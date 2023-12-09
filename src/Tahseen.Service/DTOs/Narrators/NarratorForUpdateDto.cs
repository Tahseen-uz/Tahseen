using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Narrators;

public class NarratorForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IFormFile Image { get; set; }

}
