using Microsoft.AspNetCore.Mvc;

namespace VeterinerKlinigi.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutMobileSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}