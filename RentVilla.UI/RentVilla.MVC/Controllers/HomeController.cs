using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Product;
using System.Diagnostics;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductVM> response = new();
            using (HttpClient httpClient = new())
            {
                HttpResponseMessage responseApi = await httpClient.GetAsync($"http://localhost:5006/api/products/get");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<List<ProductVM>>(contentResponseApi);
            }
            return View(response);
        }

        
    }
}
