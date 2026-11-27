using Microsoft.AspNetCore.Mvc;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}