using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Books.Publishers;

public class PublisherForCreationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public IFormFile Image { get; set; }

    public string ContactInformation { get; set; }
}
