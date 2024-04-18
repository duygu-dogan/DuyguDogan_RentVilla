using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Cart;
using RentVilla.MVC.Models.Product;
using RentVilla.MVC.Services.HttpClientService;
using System.Diagnostics;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientService _clientService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientService clientService)
        {
            _logger = logger;
            _configuration = configuration;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = await _clientService.GetHttpResponse("products/get");
            string contentResponseApi = await responseMessage.Content.ReadAsStringAsync();
            List<ProductVM>? response = JsonSerializer.Deserialize<List<ProductVM>>(contentResponseApi);
            return View(response);
        }   
    }
}
