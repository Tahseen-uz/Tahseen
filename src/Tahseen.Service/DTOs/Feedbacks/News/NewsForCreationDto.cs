using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Feedbacks.News;

public class NewsForCreationDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Media { get; set; }
    public string Author { get; set; }
}