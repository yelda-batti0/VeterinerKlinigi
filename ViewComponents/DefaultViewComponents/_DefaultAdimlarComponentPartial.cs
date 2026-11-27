using Microsoft.AspNetCore.Mvc;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultAdimlarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}