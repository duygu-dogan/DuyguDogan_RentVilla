using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Cart;
using RentVilla.MVC.Models.Product;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    [Authorize(AuthenticationSchemes ="Admin")]
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
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage httpResponse = await client.GetAsync("CartItems");
                string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                List<GetCartItemVM> cartItems = JsonSerializer.Deserialize<List<GetCartItemVM>>(contentResponse);
            }
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(string id)
        {
            string baseUrl = _configuration["API:Url"];
            ProductVM? response = new();
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await httpClient.GetAsync($"products/getbyid?ProductId={id}");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ProductVM>(contentResponseApi);
            }
            return View(response);
        }
    }
}
