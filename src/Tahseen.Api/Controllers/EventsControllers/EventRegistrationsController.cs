using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.Interfaces.IEventsServices;
using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Api.Controllers.EventsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRegistrationsController : ControllerBase
    {
        private readonly IEventRegistrationService service;

        public EventRegistrationsController(IEventRegistrationService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(EventRegistrationForCreationDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });

        [HttpPut]
        public async ValueTask<IActionResult> PutAsync(long id,EventRegistrationForUpdateDto dto) =>
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
}
