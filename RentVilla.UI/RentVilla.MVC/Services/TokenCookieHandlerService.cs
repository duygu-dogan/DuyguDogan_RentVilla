using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.DTOs;
using System.Security.Claims;

namespace RentVilla.MVC.Services
{
    public class TokenCookieHandlerService : ITokenCookieHandlerService
    { 
        public async Task TokenCookieHandler(TokenDTO token, HttpContext context = null)
        {
            var handler = new JsonWebTokenHandler();

            var jsonToken = handler.ReadToken(token.AccessToken) as JsonWebToken;

            var userName = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value; ;
                var role = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var expireAt = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration)?.Value;

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Hash, token.AccessToken),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Expiration, expireAt),
                    //new Claim(ClaimTypes.Role, role)
                };
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = token.Expiration,
                    IsPersistent = false,
                    AllowRefresh = true
                };
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
            }
    }
}
