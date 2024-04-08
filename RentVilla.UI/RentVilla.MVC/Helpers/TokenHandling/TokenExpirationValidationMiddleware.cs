using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Account;
using AspNetCoreHero.ToastNotification.Abstractions;
using RentVilla.MVC.DTOs;
using RentVilla.MVC.Services.TokenCookieService;

namespace RentVilla.MVC.Helpers.TokenHandling
{
    public class TokenExpirationValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenExpirationValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ITokenCookieHandlerService tokenCookieHandlerService, INotyfService notyfService)
        {
            if (!context.User.Identity.IsAuthenticated && context.Request.Cookies.ContainsKey("RentVilla.Cookie_RT"))
            {
                await RefreshToken(context, tokenCookieHandlerService, notyfService);
                //await _next(context);
            }
            else
            {
                await _next(context);
            }
        }
        [HttpPost]
        public async Task RefreshToken(HttpContext context, ITokenCookieHandlerService tokenService, INotyfService notyfService)
        {
            
            string refreshToken = context.Request.Cookies["RentVilla.Cookie_RT"];
            string baseUrl = _configuration["API:Url"];
            string returnUrl = context.Request.Path;
            LoginResponseVM newToken = new();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO { RefreshToken = refreshToken };
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage responseApi = await client.PostAsJsonAsync("auth/refreshToken", refreshTokenDTO);
                    if (responseApi.IsSuccessStatusCode)
                    {
                        string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                        newToken = System.Text.Json.JsonSerializer.Deserialize<LoginResponseVM>(contentResponseApi);
                        if (newToken.Token != null)
                        {
                            await tokenService.TokenCookieHandler(newToken, context);
                        }
                        else
                        {
                            notyfService.Information("Your session has expired. Please log in again.");
                            context.Response.Redirect("/Account/Login");
                        }
                    }
                    else
                    {
                        notyfService.Information("Your session has expired. Please log in again.");
                        context.Response.Redirect("/Account/Login");
                    }
                }
            }
            else
            {
                notyfService.Information("Your session has expired. Please log in again.");
                context.Response.Redirect("/Account/Login");
            }
        }
    }
}
