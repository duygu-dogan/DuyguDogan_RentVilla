using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Product;
using RentVilla.MVC.Models.Reservation;

namespace RentVilla.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly INotyfService _notyf;

        public CheckoutController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Index(AddReservationVM model, string productId, string productName, int shortestRent)
        {
            var rentPeriod = model.EndDate - model.StartDate;
            int totalDays = Convert.ToInt32(rentPeriod.TotalDays);
            var returnUrl = TempData["ReturnUrl"]?.ToString();
            if (totalDays < shortestRent)
            {
                _notyf.Error("Please select a valid date range!");
                return Redirect(returnUrl);
            }
            string? userId = User.Claims?.Where(w => w.Type.Contains("UserId")).FirstOrDefault()?.Value;
            if (!String.IsNullOrEmpty(userId))
            { model.UserId = userId; }
            else
            {
                _notyf.Error("Please login to continue!");
                return RedirectToAction("Login", "Account");
            }
            //model.Product.ProductImages[0] = new ProductImageVM { Path = productImage };
            model.Product = new ProductVM();
            model.Product.Id = productId;
            model.Product.Name = productName;
            
            decimal adultCost = totalDays * model.AdultNumber * model.ProductPrice;
            decimal childCost = Convert.ToDecimal(totalDays * model.ChildrenNumber * 0.5 * rentPeriod);
            model.TotalCost = adultCost + childCost;
            return View(model);
        }
    }
}
