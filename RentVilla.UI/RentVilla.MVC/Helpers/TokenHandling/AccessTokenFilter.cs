using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RentVilla.MVC.Services.TokenCookieService;
using ServiceStack;
using System.Net.Http.Headers;

namespace RentVilla.MVC.Helpers.TokenHandling
{
    public class AccessTokenFilter : IAsyncActionFilter
    {
        private readonly ITokenCookieHandlerService _tokenService;

        public AccessTokenFilter(ITokenCookieHandlerService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var accessToken = _tokenService.GetAccessToken();
            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    await next();
            //}
            //context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            await next();
        }
    } 
}

