using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Languages;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.Interfaces.ILanguageServices;
using Tahseen.Service.Interfaces.IReservationsServices;

namespace Tahseen.Api.Controllers.LanguagesControllers
{
    public class LanguagesController : BaseController
    {
        private readonly ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LanguageForCreationDto data)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _languageService.AddAsync(data)
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _languageService.RetrieveByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _languageService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromBody] LanguageForUpdateDto data)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _languageService.ModifyAsync(Id, data)
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _languageService.RetrieveAllAsync()
            };
            return Ok(response);
        }

    }
}
