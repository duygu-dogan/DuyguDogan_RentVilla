using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.ViewComponents
{
    public class SpinnerViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
