using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Cart;
using RentVilla.MVC.Models.Reservation;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class ReservationController : Controller
    {
        readonly ILogger<ReservationController> _logger;
        readonly IConfiguration _configuration;
        readonly INotyfService _notyf;

        public ReservationController(ILogger<ReservationController> logger, IConfiguration configuration, INotyfService notyf)
        {
            _logger = logger;
            _configuration = configuration;
            _notyf = notyf;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = userIdentity?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            using (HttpClient client = new())
            {
                string baseUrl = _configuration["API:Url"];
                client.BaseAddress = new Uri(baseUrl);
                var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage httpResponse = await client.GetAsync("CartItems");
                string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                List<GetCartItemVM> cartItems = JsonSerializer.Deserialize<List<GetCartItemVM>>(contentResponse);
                CreateReservationVM model = new()
                {
                    AppUserId = userIdClaim,
                    AdultNumber = cartItems.FirstOrDefault().AdultNumber,
                    ChildrenNumber = cartItems.FirstOrDefault().ChildrenNumber,
                    ProductId = cartItems.FirstOrDefault().Product.Id,
                    ProductName = cartItems.FirstOrDefault().Product.Name,
                    ProductPrice = cartItems.FirstOrDefault().Product.Price,
                    StartDate = cartItems.FirstOrDefault().StartDate,
                    EndDate = cartItems.FirstOrDefault().EndDate,
                    TotalCost = cartItems.FirstOrDefault().TotalCost
                };
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationVM model)
        {
            try
            {
                using (HttpClient client = new())
                {
                    string baseUrl = _configuration["API:Url"];
                    client.BaseAddress = new Uri(baseUrl);
                    var accessToken = HttpContext.Request.Cookies["RentVilla.Cookie_AT"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
                    var userId = userIdentity?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                    var requestData = new
                    {
                        createReservation = new
                        {
                            AppUserId = userId,
                            ProductId = model.ProductId,
                            ProductName = model.ProductName,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            AdultNumber = model.AdultNumber,
                            ChildrenNumber = model.ChildrenNumber,
                            Note = model.Note,
                            ProductPrice = model.ProductPrice,
                            TotalCost = model.TotalCost,
                            PaymentData = model.PaymentData
                        }
                    };
                    HttpResponseMessage httpResponse = await client.PostAsJsonAsync("Reservations", requestData);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        HttpContext.Response.Cookies.Delete("RentVilla.Cookie_SC");
                        _notyf.Success("Your payment is successful. Enjoy your stay!");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        _notyf.Error("An error occurred while processing your payment. Please try again.");
                        return RedirectToAction("Index");

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while proccessing payment", ex);
            }
        }
    }
}
