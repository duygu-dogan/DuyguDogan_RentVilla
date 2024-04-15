using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentVilla.Application.Abstraction.Services;

namespace RentVilla.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles(int page = 0, int size = 10)
        {
            var response = _roleService.GetAllRoles(page, size);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var response = await _roleService.GetRoleById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            var response = await _roleService.CreateRole(name);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] string id, string name)
        {
            var response = await _roleService.UpdateRole(id, name);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var response = await _roleService.DeleteRole(id);
            return Ok(response);
        }

    }
}
