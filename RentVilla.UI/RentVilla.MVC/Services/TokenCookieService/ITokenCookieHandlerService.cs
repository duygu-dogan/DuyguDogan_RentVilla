using RentVilla.MVC.DTOs;
using RentVilla.MVC.Models.Account;

namespace RentVilla.MVC.Services.TokenCookieService
{
    public interface ITokenCookieHandlerService
    {
        Task TokenCookieHandler(LoginResponseVM token, HttpContext? context = null);
        string GetAccessToken();
    }
}
