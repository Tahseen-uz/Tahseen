using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.Interfaces.IReservationsServices;

namespace Tahseen.Api.Controllers.ReservationsControllers;

public class ReservationController:BaseController
{
    private readonly IReservationsService _reservationService;
    public ReservationController(IReservationsService reservationService)
    {
        _reservationService =reservationService;
    }
    
          [HttpPost]
          public async Task<IActionResult> PostAsync([FromBody]ReservationForCreationDto data)
          {
              var response = new Response()
              {
                  StatusCode = 200,
                  Message = "Success",
                  Data = await _reservationService.AddAsync(data)
              };
              return Ok(response);
          }
          
          [HttpGet("{id}")]
          public async Task<IActionResult> GetAsync([FromRoute]long Id)
          {
              var response = new Response()
              {
                  StatusCode = 200,
                  Message = "Success",
                  Data = await _reservationService.RetrieveByIdAsync(Id)
              };
              return Ok(response);
          }
          
          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteAsync([FromRoute]long Id)
          {
              var response = new Response()
              {
                  StatusCode = 200,
                  Message = "Success",
                  Data = await _reservationService.RemoveAsync(Id)
              };
              return Ok(response);
          }
  
          [HttpPut("{id}")]
          public async Task<IActionResult> PutAsync([FromRoute]long Id, 
              [FromBody]ReservationForUpdateDto data)
          {
              var response = new Response()
              {
                  StatusCode = 200,
                  Message = "Success",
                  Data = await _reservationService.ModifyAsync(Id, data)
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
                  Data = _reservationService.RetrieveAllAsync()
              };
              return Ok(response);
          }
    
}