using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Feature.Commands.AppUser.LoginUser;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            throw new ArgumentOutOfRangeException();

            try
            {
                LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
