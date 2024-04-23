using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Region;
using RentVilla.MVC.Services.HttpClientService;
using System.Text.Json;

namespace RentVilla.MVC.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientService _clientService;

        public RegionsController(IHttpClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = await _clientService.GetHttpResponse("region/getallstates");
            string contentResponseApi = await responseMessage.Content.ReadAsStringAsync();
            List<StateVM>? response = JsonSerializer.Deserialize<List<StateVM>>(contentResponseApi);

            return View(response);
        }
    }
}
