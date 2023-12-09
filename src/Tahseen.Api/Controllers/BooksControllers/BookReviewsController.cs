using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers;

public class BookReviewsController : BaseController
{
    private readonly IBookReviewService service;

    public BookReviewsController(IBookReviewService service)
    {
        this.service = service;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookReviewForCreationDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.AddAsync(dto)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BookReviewForUpdateDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.ModifyAsync(id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RemoveAsync(id)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveByIdAsync(id)
        });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAllAsync()
        });
}
