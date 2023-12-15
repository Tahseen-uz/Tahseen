using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Librarians;
using Tahseen.Service.Interfaces.ILibrariansService;

namespace Tahseen.Api.Controllers.LibrariesControllers;
//[Authorize(Roles = "Librarian")]

public class LibrarianController:BaseController
{
    
    private readonly ILibrarianService _librarianService;
    
    public LibrarianController(ILibrarianService librarianService)
    {
        _librarianService =librarianService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm]LibrarianForCreationDto data)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.AddAsync(data)
        };
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")]long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }
          
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.RemoveAsync(Id)
        };
        return Ok(response);
    }
  
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromForm]LibrarianForUpdateDto data)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.ModifyAsync(Id, data)
        };
        return Ok(response);
    }
          
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.RetrieveAllAsync(@params)
        };
        return Ok(response);
    }


    [HttpGet("me")]
    public async Task<IActionResult> GetLoggedAsync()
    {
        var librarianId = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _librarianService.RetrieveByIdAsync(librarianId)
        };
        return Ok(response);
    }

}
