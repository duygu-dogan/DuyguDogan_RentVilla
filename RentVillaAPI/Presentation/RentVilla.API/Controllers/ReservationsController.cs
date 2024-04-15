using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Consts;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.Enums;
using RentVilla.Application.Feature.Commands.Reservations.CancelReservation;
using RentVilla.Application.Feature.Commands.Reservations.CreateReservation;
using RentVilla.Application.Feature.Commands.Reservations.UpdateReservationStatus;
using RentVilla.Application.Feature.Queries.Reservations.GetActiveReservations;
using RentVilla.Application.Feature.Queries.Reservations.GetPassiveReservations;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Reservations, Definition = "Creates new reservation", ActionType = ActionTypes.Writing)]
        public async Task<ActionResult> CreateReservation(CreateReservationCommandRequest request)
        {
           CreateReservationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Reservations, Definition = "Gets active reservations", ActionType = ActionTypes.Reading)]
        public async Task<ActionResult> GetActiveReservations([FromQuery] GetActiveReservationsQueryRequest getReservationsQueryRequest)
        {
            GetActiveReservationsQueryResponse response = await _mediator.Send(getReservationsQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Reservations, Definition = "Gets passive reservations", ActionType = ActionTypes.Reading)]
        public async Task<ActionResult> GetPassiveReservations([FromQuery] GetPassiveReservationsQueryRequest getPassiveReservationsQueryRequest)
        {
            GetPassiveReservationsQueryResponse response = await _mediator.Send(getPassiveReservationsQueryRequest);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Reservations, Definition = "Updates reservation status", ActionType = ActionTypes.Updating)]
        public async Task<ActionResult> UpdateReservationStatus([FromBody] UpdateReservationStatusCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Reservations, Definition = "Deletes given reservation", ActionType = ActionTypes.Deleting)]
        public async Task<ActionResult> CancelReservation([FromQuery] CancelReservationCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
