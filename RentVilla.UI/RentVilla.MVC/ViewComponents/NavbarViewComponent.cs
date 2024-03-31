using Microsoft.AspNetCore.Mvc;

namespace RentVilla.MVC.ViewComponents
{
    public class NavbarViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
