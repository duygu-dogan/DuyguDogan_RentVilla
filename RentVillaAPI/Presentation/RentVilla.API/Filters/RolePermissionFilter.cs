using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.CustomAttributes;
using RentVilla.Application.DTOs.AuthConfigurationDTOs;
using RentVilla.Domain.Entities.Concrete;
using System.Globalization;
using System.Reflection;

namespace RentVilla.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        private readonly IUserService _service;

        public RolePermissionFilter(IUserService service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userName = context.HttpContext.User.Identity?.Name;
            if(!String.IsNullOrEmpty(userName))
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                var httpAttribute = descriptor.MethodInfo.GetCustomAttributes(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
                var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute?.Menu}.{attribute?.ActionType}";
                var hasRole = await _service.HasRolePermissionToEndpointAsync(userName, code);
                if(!hasRole)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    await next();
                }
            }
            await next();
        }
    }
}
