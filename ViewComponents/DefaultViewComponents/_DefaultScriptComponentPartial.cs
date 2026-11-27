using Microsoft.AspNetCore.Mvc;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}