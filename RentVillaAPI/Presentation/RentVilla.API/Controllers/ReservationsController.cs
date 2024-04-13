using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Feature.Commands.Reservations.CreateReservation;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ReservationsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(CreateReservationCommandRequest request)
        {
           CreateReservationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
