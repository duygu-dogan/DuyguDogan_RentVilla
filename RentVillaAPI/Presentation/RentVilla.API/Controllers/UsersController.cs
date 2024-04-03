using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Feature.Commands.AppUser.AddUser;
using RentVilla.Application.Feature.Commands.AppUser.LoginUser;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserCommandRequest addUserCommandRequest)
        {
            addUserCommandRequest.BirthDate = addUserCommandRequest.BirthDate.ToUniversalTime();
            AddUserCommandResponse response = await _mediator.Send(addUserCommandRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
    }
}
