using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentVilla.MVC.Models.Address;
using RentVilla.MVC.Models.Product;
using RentVilla.MVC.Services.HttpClientService;

namespace RentVilla.MVC.ViewComponents
{
    public class SearchbarViewComponent: ViewComponent
    {
        private readonly IHttpClientService _clientService;

        public SearchbarViewComponent(IHttpClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductFilterVM model = new();
            HttpResponseMessage stateResponseMessage = await _clientService.GetHttpResponse("Region/GetAllStates");
            if (stateResponseMessage.IsSuccessStatusCode)
            {
                var states = await stateResponseMessage.Content.ReadFromJsonAsync<List<UserAddressStateVM>>();
                foreach (var state in states)
                {
                    model.ProductStateList.Add(new SelectListItem { Text = state.Name, Value = state.Id });
                }
            }
            HttpResponseMessage attResponseMessage = await _clientService.GetHttpResponse("Attributes/GetTypes");
            if (attResponseMessage.IsSuccessStatusCode)
            {
                var attributeTypes = await attResponseMessage.Content.ReadFromJsonAsync<List<ProductAttributeTypeVM>>();
                foreach (var types in attributeTypes)
                {
                    model.ProductAttributeTypeList.Add(new SelectListItem { Text = types.TypeName, Value = types.Id });
                }
            }
            return View(model);
        }
    }
}
