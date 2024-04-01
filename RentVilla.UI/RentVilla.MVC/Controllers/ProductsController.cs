using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Product;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class ProductsController : Controller
    {
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
        public async Task<IActionResult> GetById(string id)
        {             ProductVM response = new();
                   using (HttpClient httpClient = new())
            {
                HttpResponseMessage responseApi = await httpClient.GetAsync($"http://localhost:5006/api/products/getbyid/{id}");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ProductVM>(contentResponseApi);
            }
            return View(response);
        }
    }
}
