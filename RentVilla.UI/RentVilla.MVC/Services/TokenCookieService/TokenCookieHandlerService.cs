using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.Models.Account;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.DTOs;
using System.Reflection;

namespace RentVilla.MVC.Services.TokenCookieService
{
    public class TokenCookieHandlerService : ITokenCookieHandlerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        private readonly INotyfService _notyfService;

        public TokenCookieHandlerService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task TokenCookieHandler(LoginResponseVM model, HttpContext? context = null)
        {
            try
            {
                var handler = new JsonWebTokenHandler();
                var jsonToken = handler.ReadToken(model.Token.AccessToken) as JsonWebToken;

                var claims = jsonToken.Claims;

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = model.Token.RefreshTokenEndDate,
                    IsPersistent = true,
                    AllowRefresh = false
                };

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                context.User = principal;
                context.Response.Cookies.Append("RentVilla.Cookie_AT", model.Token.AccessToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    //SameSite = SameSiteMode.Strict,
                    Expires = model.Token.Expiration
                });
                if(context.Request.Cookies["RentVilla.Cookie_RT"] != null)
                context.Response.Cookies.Delete("RentVilla.Cookie_RT");
                context.Response.Cookies.Append("RentVilla.Cookie_RT", model.Token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    //SameSite = SameSiteMode.Strict,
                    Expires = model.Token.RefreshTokenEndDate
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public string GetAccessToken()
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            var accessToken = context.Request.Cookies["RentVilla.Cookie_AT"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                var expirationDate = context.User.Claims.Where(c => c.Type == "expiration").FirstOrDefault().Value;
                if (expirationDate == null || DateTimeOffset.Now >= DateTimeOffset.Parse(expirationDate))
                {
                    var refreshToken = context.Request.Cookies["RentVilla.Cookie_RT"];
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        JsonWebTokenHandler handler = new JsonWebTokenHandler();
                        var refreshJsonToken = handler.ReadToken(refreshToken) as JsonWebToken;
                        var refreshExpirationDate = context.User.Claims.Where(c => c.Type.EndsWith("expiration")).FirstOrDefault().Value;
                        if (DateTime.UtcNow >= DateTime.Parse(refreshExpirationDate))
                        {
                            context.Response.Cookies.Delete("RentVilla.Cookie_RT");
                            return null;
                        }
                        else
                            RefreshToken(context, refreshToken);
                    }
                }
                return accessToken;
            }
            return null;
        }
        [HttpPost]
        public async Task RefreshToken(HttpContext context, string refreshToken)
        {
            string baseUrl = _configuration["API:Url"];
            string returnUrl = context.Request.Path;
            LoginResponseVM newToken = new();
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
                        await TokenCookieHandler(newToken, context);
                    }
                    else
                    {
                        _notyfService.Information("Your session has expired. Please log in again.");
                        context.Response.Redirect("/Account/Login");
                    }
                }
                else
                {
                    _notyfService.Information("Your session has expired. Please log in again.");
                    context.Response.Redirect("/Account/Login");
                }
            }
        }
    }
}
