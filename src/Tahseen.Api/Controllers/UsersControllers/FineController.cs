using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class FineController : BaseController
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _fineService.RetrieveAllAsync()
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
                Data = await _fineService.RetrieveByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FineForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _fineService.AddAsync(dto)
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
                Data = await _fineService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromBody]FineForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _fineService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
    }
}
