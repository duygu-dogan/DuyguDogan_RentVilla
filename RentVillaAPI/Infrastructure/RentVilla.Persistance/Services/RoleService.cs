using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Abstraction.Services.AuthConfigurations;
using RentVilla.Application.Repositories.EndpointRepo;
using RentVilla.Application.Repositories.MenuRepo;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        private readonly IAuthConfigService _configService;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;

        public RoleService(RoleManager<AppRole> roleManager, IAuthConfigService configService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository)
        {
            _roleManager = roleManager;
            _configService = configService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
        }

        public async Task AssignRoleToEndpointAsync(string[] roleIds, string code, Type type, string menu)
        {
            try
            {
                Menu? _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
                if (_menu == null)
                {
                    _menu = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = menu
                    };
                    await _menuWriteRepository.AddAsync(_menu);
                    await _menuWriteRepository.SaveAsync();
                }
                Endpoint? endpoint = await _endpointReadRepository.AppDbContext.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e=> e.Menu.Name == menu && e.Code == code);
                if (endpoint == null)
                {
                    var action = _configService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(m => m.Name == menu).Actions.FirstOrDefault(e => e.Code == code);
                    endpoint = new()
                    {
                        Code = code,
                        ActionType = action.ActionType,
                        HttpType = action.HttpType,
                        Id = Guid.NewGuid(),
                        Definition = action.Definition,
                        Menu = _menu
                    };
                    await _endpointWriteRepository.AddAsync(endpoint);
                    await _endpointWriteRepository.SaveAsync();
                }

                foreach (var role in endpoint.Roles)
                {
                    endpoint.Roles.Remove(role);
                }


                var appRoles = await _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
                foreach (var role in appRoles)
                {
                    endpoint.Roles.Add(role);
                }
                await _endpointWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CreateRole(string name)
        {
            try
            {
                IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRole(string id)
        {
            try
            {
                AppRole role = await _roleManager.FindByIdAsync(id);
                IdentityResult result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> GetAllRoles(int page, int size)
        {
           var roles = _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);
            return roles.Skip(page * size).Take(size).ToDictionary(role => role.Key, role => role.Value);
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint endpoint = await _endpointReadRepository.AppDbContext.Include(e => e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu );
            if(endpoint == null)
            {
                return new();
            }
           return endpoint.Roles.Select(r => r.Id).ToList();
        }

        public async Task<bool> UpdateRole(string id, string name)
        {
            try
            {
                IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
