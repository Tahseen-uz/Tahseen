using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers;

[Route("api/[controller]")]
[ApiController]
public class BookReviewsController : ControllerBase
{
    private readonly IBookReviewService service;

    public BookReviewsController(IBookReviewService service)
    {
        this.service = service;
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(BookReviewForCreationDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.AddAsync(dto)
        });

    [HttpPut]
    public async ValueTask<IActionResult> PutAsync(long id,BookReviewForUpdateDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.ModifyAsync(id, dto)
        });

    [HttpDelete("id")]
    public async ValueTask<IActionResult> DeleteAsync(long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RemoveAsync(id)
        });

    [HttpGet("id")]
    public async ValueTask<IActionResult> GetByIdAsync(long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveByIdAsync(id)
        });

    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync() =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = this.service.RetrieveAll()
        });
}
