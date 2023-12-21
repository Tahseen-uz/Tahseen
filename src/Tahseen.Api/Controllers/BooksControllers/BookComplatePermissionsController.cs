using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.BookComplatePermissions;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers;

public class BookComplatePermissionsController : BaseController
{
    private readonly IBookComplatePermissionService _bookComplatePermissionService;

    public BookComplatePermissionsController(IBookComplatePermissionService bookComplatePermissionService)
    {
        _bookComplatePermissionService = bookComplatePermissionService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookComplatePermissionForCreationDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookComplatePermissionService.AddAsync(dto)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BookComplatePermissionForUpdateDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookComplatePermissionService.ModifyAsync(id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookComplatePermissionService.RemoveAsync(id)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookComplatePermissionService.RetrieveByIdAsync(id)
        });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams @params) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._bookComplatePermissionService.RetrieveAllAsync(@params)
        });
}
