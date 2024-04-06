using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Product;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string baseUrl = _configuration["API:Url"];
            List<ProductVM>? response = new();
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await httpClient.GetAsync("products/get");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<List<ProductVM>>(contentResponseApi);
            }
            return View(response);
        }
        public async Task<IActionResult> GetById(string id)
        {
            string baseUrl = _configuration["API:Url"];
            ProductVM? response = new();
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await httpClient.GetAsync($"products/getbyid/{id}");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ProductVM>(contentResponseApi);
            }
            return View(response);
        }
    }
}
