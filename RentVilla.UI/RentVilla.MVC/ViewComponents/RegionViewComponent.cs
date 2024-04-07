using Microsoft.AspNetCore.Mvc;
using RentVilla.MVC.Models.Region;
using System.Text.Json;

namespace RentVilla.MVC.ViewComponents
{
    public class RegionViewComponent: ViewComponent
    {
        private readonly IConfiguration _configuration;

        public RegionViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                string baseUrl = _configuration["API:Url"];
                List<StateVM> response = new();
                using (HttpClient httpClient = new())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage responseApi = await httpClient.GetAsync("region/getallstates");
                    string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<List<StateVM>>(contentResponseApi);
                }
                return View(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
