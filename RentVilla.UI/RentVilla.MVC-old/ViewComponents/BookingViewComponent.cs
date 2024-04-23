using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.ViewComponents
{
    public class BookingViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
