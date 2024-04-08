using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.DTOs;
using System.Security.Claims;
using System.Reflection;
using RentVilla.MVC.Models.Account;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RentVilla.MVC.Services.TokenCookieService
{
    public class TokenCookieHandlerService : ITokenCookieHandlerService
    {
        public async Task TokenCookieHandler(LoginResponseVM model, HttpContext? context = null)
        {
            var handler = new JsonWebTokenHandler();
            var jsonToken = handler.ReadToken(model.Token.AccessToken) as JsonWebToken;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, model.Token.AccessToken),
                new Claim(ClaimTypes.Name, model.UserData.UserName),
                new Claim(ClaimTypes.Expiration, model.Token.Expiration.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var userIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = model.Token.Expiration,
                IsPersistent = true,
                AllowRefresh = false
            };

            await context.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
        }

    }
}
