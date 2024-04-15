using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Abstraction.Services.AuthConfigurations;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.DTOs.AuthConfigurationDTOs;
using RentVilla.Application.Enums;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class AuthConfigsController : ControllerBase
    {
        private readonly IAuthConfigService _configService;
        private readonly IRoleService _roleService;

        public AuthConfigsController(IAuthConfigService configService, IRoleService roleService)
        {
            _configService = configService;
            _roleService = roleService;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Gets authorize definition endpoints", Menu = "AuthConfig")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
           var datas = _configService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
        [HttpPost]
        public async Task<IActionResult> AssingRoleEndpoint(AssignRoleDTO assignRoleDTO)
        {
            assignRoleDTO.Type = typeof(Program);
            await _roleService.AssignRoleToEndpointAsync(assignRoleDTO.RoleIds, assignRoleDTO.Code, assignRoleDTO.Type, assignRoleDTO.Menu);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetRolesToEndpoint(string code, string menu)
        {
            var roles = await _roleService.GetRolesToEndpointAsync(code, menu);
            return Ok(roles);
        }
    }
}
