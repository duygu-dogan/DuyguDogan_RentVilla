using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.Models.Reservation;

namespace RentVilla.MVC.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index(CreateReservationVM model)
        {
            var handler = new JsonWebTokenHandler();
            var token = HttpContext.Request.Cookies["RentVilla.Cookie"];
            JsonWebToken? jsonToken = handler.ReadToken(token) as JsonWebToken;
            model.AppUserId = jsonToken.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;
            return View(model);
        }
    }
}
