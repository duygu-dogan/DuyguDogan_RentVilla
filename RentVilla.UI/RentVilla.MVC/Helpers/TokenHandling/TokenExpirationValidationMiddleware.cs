using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Account;
using RentVilla.MVC.Services;

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

        public async Task Invoke(HttpContext context, ITokenCookieHandlerService tokenCookieHandlerService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var claims = context.User.Claims.ToList();
                var expiresAtClaim = claims.Where(x => x.Type.Contains("expiration")).FirstOrDefault();
                if (expiresAtClaim != null && DateTime.TryParse(expiresAtClaim.Value, out DateTime expiresAt))
                {
                    if (expiresAt <= DateTime.UtcNow)
                    {
                        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        context.Response.Redirect("/Account/Login");
                        return;
                    }
                }
            }
            else if (context.Response.StatusCode == ((int)HttpStatusCode.Unauthorized))
            {
                await RefreshToken(context, tokenCookieHandlerService);
                return;
            }

            await _next(context);
        }
        [HttpPost]
        public async Task RefreshToken(HttpContext context, ITokenCookieHandlerService tokenCookieHandlerService)
        {
            string refreshToken = context.Request.Cookies["RentVilla.Cookies_RT"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                context.Response.Redirect("/Account/RefreshTokenLogin");
                return;
            }
            string returnUrl = context.Request.Path;

            string baseUrl = _configuration["API:Url"];
            TokenVM newToken = new();

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await client.PostAsJsonAsync("auth/refreshToken", refreshToken);
                if (responseApi.IsSuccessStatusCode)
                {
                    string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                    newToken = System.Text.Json.JsonSerializer.Deserialize<TokenVM>(contentResponseApi);
                    if (newToken.Token.RefreshToken != null)
                    {
                        await tokenCookieHandlerService.TokenCookieHandler(newToken.Token, context);
                        context.Response.Cookies.Append("RentVilla.Cookies_RT", newToken.Token.RefreshToken);
                        context.Response.Redirect(returnUrl);
                    }
                    else
                    {
                        context.Response.Redirect("/Account/Login");
                    }
                }
            }
        }
    }
}
