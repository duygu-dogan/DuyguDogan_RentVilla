using RentVilla.Application.DTOs.AuthConfigurationDTOs;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Services
{
    public interface IRoleService
    {
        Dictionary<string, string> GetAllRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string id, string name);
        Task AssignRoleToEndpointAsync(string[] roleIds, string code, Type type, string menu);
        Task<List<GetRolesDTO>> GetRolesToEndpointAsync(string code, string menu);
    }
}
