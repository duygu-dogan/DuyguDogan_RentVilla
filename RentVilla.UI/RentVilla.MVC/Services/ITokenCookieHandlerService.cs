using RentVilla.MVC.DTOs;
using RentVilla.MVC.Models.Account;

namespace RentVilla.MVC.Services
{
    public interface ITokenCookieHandlerService
    {
        Task TokenCookieHandler(LoginResponseVM token, HttpContext context = null);
    }
}
