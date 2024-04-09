using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Product;
using RentVilla.MVC.Models.Reservation;

namespace RentVilla.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index(AddReservationVM model)
        {
            return View(model);
        }
    }
}
