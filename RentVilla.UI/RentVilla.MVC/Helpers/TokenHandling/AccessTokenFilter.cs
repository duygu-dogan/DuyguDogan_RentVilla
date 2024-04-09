using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RentVilla.MVC.Services.TokenCookieService;
using System.Net.Http.Headers;

namespace RentVilla.MVC.Helpers.TokenHandling
{
    public class AccessTokenFilterAttribute : ActionFilterAttribute
    {
        [AccessTokenFilterAttribute(Order = int.MinValue)]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var accessToken = request.Cookies["RentVilla.Cookie_AT"];

            if (filterContext.ActionDescriptor.EndpointMetadata.Contains("localhost:5006/api"))
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers["Authorization"] = $"Bearer {accessToken}";
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}

