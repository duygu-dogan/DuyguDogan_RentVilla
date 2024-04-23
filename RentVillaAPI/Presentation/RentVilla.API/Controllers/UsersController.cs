using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.DTOs.UserDTOs;
using RentVilla.Application.Enums;
using RentVilla.Application.Feature.Commands.AppUser.CreateUser;
using RentVilla.Application.Feature.Commands.AppUser.LoginUser;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        private readonly IUserService _userService;

        public UsersController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            createUserCommandRequest.BirthDate = createUserCommandRequest.BirthDate.ToUniversalTime();
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Gets all users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers(int page, int size)
        {
            var users = await _userService.GetAllUsersAsync(page, size);
            return Ok(users);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Assings roles to user", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserDTO assignRoleToUserDTO)
        {
            await _userService.AssignRoleToUserAsync(assignRoleToUserDTO.UserId, assignRoleToUserDTO.RoleIds);
            return Ok();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Gets user roles", Menu = "Users")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var roles = await _userService.GetUserRoles(userId);
            return Ok(roles);
        }
    }
}
