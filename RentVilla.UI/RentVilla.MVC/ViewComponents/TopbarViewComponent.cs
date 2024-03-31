using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.ViewComponents
{
    public class TopbarViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
