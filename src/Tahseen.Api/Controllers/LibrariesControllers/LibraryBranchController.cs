using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.Interfaces.ILibrariesService;
using Tahseen.Service.Services.Libraries;

namespace Tahseen.Api.Controllers.LibrariesControllers;

public class LibraryBranchController : BaseController
{
    private readonly ILibraryBranchService _libraryBranchService;
    
    public LibraryBranchController(ILibraryBranchService libraryBranchService)
    {
        _libraryBranchService = libraryBranchService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm]LibraryBranchForCreationDto data)
    {
        var response = new Response()
        {   
            StatusCode = 200,
            Message = "Success",
            Data = await _libraryBranchService.AddAsync(data)
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
            Data = await _libraryBranchService.RetrieveByIdAsync(Id)
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
            Data = await _libraryBranchService.RemoveAsync(Id)
        };
        return Ok(response);
    }
  
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromForm]LibraryBranchForUpdateDto data)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _libraryBranchService.ModifyAsync(Id, data)
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
            Data = await this._libraryBranchService.RetrieveAllAsync(@params)        
        };
        return Ok(response);
    }
    
}