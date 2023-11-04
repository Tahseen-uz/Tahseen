using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Commons;

namespace Tahseen.Service.DTOs.Feedbacks.News;

public class NewsForUpdateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Media { get; set; }
    public string Author { get; set; }
}