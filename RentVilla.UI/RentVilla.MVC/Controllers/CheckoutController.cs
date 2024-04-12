using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Cart;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IConfiguration _configuration;
        public CheckoutController(INotyfService notyf, IConfiguration configuration)
        {
            _notyf = notyf;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new())
            {
                string baseUrl = _configuration["API:Url"];
                client.BaseAddress = new Uri(baseUrl);
                var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage httpResponse = await client.GetAsync("CartItems");
                string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                List<GetCartItemVM> cartItems = JsonSerializer.Deserialize<List<GetCartItemVM>>(contentResponse);
                var shoppingCart = new ReservationCartVM();
                shoppingCart.CartItems = cartItems;
                HttpContext.Response.Cookies.Append("RentVilla.Cookie_SC", cartItems.Count().ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return View(shoppingCart);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddCartItemVM model, int shortestRent)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _notyf.Error("Please login to continue!");
                string returnUrl = $"/Products/GetDetails/{model.ProductId}";

                return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });

            }
            var rentPeriod = model.EndDate - model.StartDate;
            int totalDays = Convert.ToInt32(rentPeriod.TotalDays);
            if (totalDays < shortestRent)
            {
                _notyf.Error("Please select a valid date range!");
                return RedirectToAction("GetDetails", "Products", new {id = model.ProductId});
            }
            

            double adultCost = Convert.ToDouble(totalDays * model.AdultNumber * model.ProductPrice);
            double childCost = Convert.ToDouble(totalDays * model.ChildrenNumber * model.ProductPrice) * 0.5;
            model.TotalCost = Convert.ToDecimal(adultCost + childCost);

            using (HttpClient client = new())
            {
                string baseUrl = _configuration["API:Url"];
                client.BaseAddress = new Uri(baseUrl);
                var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                var requestData = new
                {
                    cartItemDTO = new
                    {
                        productId = model.ProductId,
                        startDate = model.StartDate,
                        endDate = model.EndDate,
                        adultNumber = model.AdultNumber,
                        childrenNumber = model.ChildrenNumber,
                        note = model.Note,
                        price = model.ProductPrice,
                        totalCost = model.TotalCost
                    }
                };
                HttpResponseMessage httpResponse = await client.PostAsJsonAsync("CartItems", requestData);
                if (httpResponse.IsSuccessStatusCode)
                {
                    _notyf.Success("Reservation request added to chart successfully. You can complete your payment.");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error("An error occurred while adding request. Please try again.");
                    return RedirectToAction("GetDetails", "Product", model);

                }
            }
        }
        public async Task<IActionResult> UpdateItemInCart(string cartItemId, int _adultNumber, int _childrenNumber, int rentDays)
        {
            try
            {
                using (HttpClient client = new())
                {
                    string baseUrl = _configuration["API:Url"];
                    client.BaseAddress = new Uri(baseUrl);
                    var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    HttpResponseMessage httpResponse = await client.GetAsync($"CartItems/{cartItemId}");
                    string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    GetCartItemVM model = JsonSerializer.Deserialize<GetCartItemVM>(contentResponse);
                    model.AdultNumber = _adultNumber;
                    model.ChildrenNumber = _childrenNumber;
                    double adultCost = Convert.ToDouble(rentDays * _adultNumber * model.Price);
                    double childCost = Convert.ToDouble(rentDays * _childrenNumber * model.Price) * 0.5;
                    model.TotalCost = Convert.ToDecimal(adultCost + childCost);
                    var requestData = new
                    {
                        cartItemDTO = new
                        {
                            cartItemId = model.CartItemId,
                            startDate = model.StartDate,
                            endDate = model.EndDate,
                            adultNumber = _adultNumber,
                            childrenNumber = _childrenNumber,
                            note = model.Note,
                            price = model.Price,
                            totalCost = model.TotalCost
                        }
                    };
                    HttpResponseMessage httpResponse2 = await client.PutAsJsonAsync("CartItems", requestData);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        _notyf.Success("Reservation choices edited successfully. You can complete your payment.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notyf.Error("An error occurred while editing request. Please try again.");
                        return RedirectToAction("GetDetails", "Product", model);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> DeleteFromCart(string cartItemId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string baseUrl = _configuration["API:Url"];
                    client.BaseAddress = new Uri(baseUrl);
                    var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    HttpResponseMessage httpResponse = await client.DeleteAsync($"CartItems/{cartItemId}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        _notyf.Success("Item removed from cart successfully.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notyf.Error("An error occurred while removing item from cart. Please try again.");
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
