using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.Models.Account;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace RentVilla.MVC.Services.TokenCookieService
{
    public class TokenCookieHandlerService : ITokenCookieHandlerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenCookieHandlerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
                    SameSite = SameSiteMode.Strict,
                    Expires = model.Token.Expiration
                });
                if(context.Request.Cookies["RentVilla.Cookie_RT"] != null)
                context.Response.Cookies.Delete("RentVilla.Cookie_RT");
                context.Response.Cookies.Append("RentVilla.Cookie_RT", model.Token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
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
            return context.Request.Cookies["RentVilla.Cookie_AT"];
        }
    }
}
