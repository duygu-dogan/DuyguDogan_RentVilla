using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return File("~/admin/index.html", "text/html");
        }
    }
}
