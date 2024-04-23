using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentVilla.MVC.Models;

namespace RentVilla.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string messagesJson = "")
        {
            var errorMessages = JsonConvert.DeserializeObject<ErrorViewModel>(messagesJson);
            ViewBag.errorMessages = errorMessages;
            return View();
        }
    }
}
