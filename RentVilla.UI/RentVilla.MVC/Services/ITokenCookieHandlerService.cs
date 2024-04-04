using RentVilla.MVC.DTOs;

namespace RentVilla.MVC.Services
{
    public interface ITokenCookieHandlerService
    {
        Task TokenCookieHandler(TokenDTO token, HttpContext context = null);
    }
}
