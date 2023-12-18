using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.BookRentalPermission;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers;

public class BookRentalPermissionsController : BaseController
{
    private readonly IBookRentalPermissionService service;

    public BookRentalPermissionsController(IBookRentalPermissionService service)
    {
        this.service = service;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookRentalPermissionForCreationDto dto) =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.AddAsync(dto)
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
    public async Task<IActionResult> GetAll() =>
        Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAllAsync()
        });
}
