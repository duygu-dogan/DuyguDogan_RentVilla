using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Cart;
using RentVilla.MVC.Models.Reservation;
using RentVilla.MVC.Services.HttpClientService;
using System.Security.Claims;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class ReservationController : Controller
    {
        readonly ILogger<ReservationController> _logger;
        readonly IConfiguration _configuration;
        readonly INotyfService _notyf;
        private readonly IHttpClientService _clientService;

        public ReservationController(ILogger<ReservationController> logger, IConfiguration configuration, INotyfService notyf, IHttpClientService clientService)
        {
            _logger = logger;
            _configuration = configuration;
            _notyf = notyf;
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = userIdentity?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            HttpResponseMessage responseMessage = await _clientService.GetHttpResponse("CartItems");
            string contentResponse = await responseMessage.Content.ReadAsStringAsync();
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
        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationVM model)
        {
            try
            {
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
            HttpResponseMessage responseMessage = await _clientService.PostHttpRequest("Reservations/CreateReservation", requestData);
                  
                if (responseMessage.IsSuccessStatusCode)
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
            catch (Exception ex)
            {
                throw new Exception("An error occured while proccessing payment", ex);
            }
        }
    }
}
