using Microsoft.AspNetCore.Mvc;

namespace VeterinerKlinigi.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}