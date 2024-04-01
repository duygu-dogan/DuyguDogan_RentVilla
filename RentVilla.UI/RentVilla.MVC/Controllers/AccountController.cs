using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
