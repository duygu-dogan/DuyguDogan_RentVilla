using RentVilla.MVC.Services.TokenCookieService;

namespace RentVilla.MVC.Helpers.TokenHandling
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenCookieHandlerService tokenService)
        {
            var accessToken = tokenService.GetAccessToken();
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            await _next(context);
        }
    }

}
