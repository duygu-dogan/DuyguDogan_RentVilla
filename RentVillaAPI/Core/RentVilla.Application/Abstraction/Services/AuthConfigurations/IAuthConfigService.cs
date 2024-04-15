using RentVilla.Application.DTOs.AuthConfigurationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Abstraction.Services.AuthConfigurations
{
    public interface IAuthConfigService
    {
        List<MenuDTO> GetAuthorizeDefinitionEndpoints(Type assemblyType);
    }
}
