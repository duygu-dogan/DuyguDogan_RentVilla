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
            var handler = new JsonWebTokenHandler();
            var jsonToken = handler.ReadToken(model.Token.AccessToken) as JsonWebToken;

            context.Response.Cookies.Append("RentVilla.Cookie_AT", model.Token.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = model.Token.RefreshTokenEndDate
            });
            var claims = jsonToken.Claims;

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(userIdentity);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = model.Token.Expiration,
                IsPersistent = true,
                AllowRefresh = false
            };

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            await context.AuthenticateAsync();
            context.Response.Cookies.Append("RentVilla.Cookie_RT", model.Token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = model.Token.RefreshTokenEndDate
            });
        }
    }
}
