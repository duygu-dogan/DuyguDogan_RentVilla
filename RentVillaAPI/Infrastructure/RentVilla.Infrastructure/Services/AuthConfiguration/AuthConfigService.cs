using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RentVilla.Application.Abstraction.Services.AuthConfigurations;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.DTOs.AuthConfigurationDTOs;
using RentVilla.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Infrastructure.Services.AuthConfiguration
{
    public class AuthConfigService : IAuthConfigService
    {
        public List<MenuDTO> GetAuthorizeDefinitionEndpoints(Type assemblyType)
        {
          Assembly assembly = Assembly.GetAssembly(assemblyType);
           var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<MenuDTO> menus = new();
            if(controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if(actions != null)
                    {
                        foreach (var action in actions)
                        {
                          var attributes = action.GetCustomAttributes(true);
                            if(attributes != null)
                            {
                               MenuDTO menu = null;

                               var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                                ActionDTO actionDTO = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionTypes), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };
                                
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if(httpAttribute != null)
                                {
                                    actionDTO.HttpType = httpAttribute.HttpMethods.First();
                                }else
                                {
                                    actionDTO.HttpType = HttpMethods.Get;
                                }

                                actionDTO.Code = $"{actionDTO.HttpType}.{menu.Name}.{actionDTO.ActionType}";
                                
                                menu.Actions.Add(actionDTO);
                            }
                        }
                    }
                }
            }
            
            return menus;
        }
    }
}
