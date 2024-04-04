using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace RentVilla.MVC.Helpers.TokenHandling
{
    public class TokenExpirationValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenExpirationValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

            await _next(context);
        }
    }
}
