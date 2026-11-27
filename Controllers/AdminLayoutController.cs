using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class AdminLayoutController : Controller
    {
        // AdminLayout/Index.cshtml bir layout dosyası (içinde RenderBody var) olduğu için
        // tek başına açılamaz. Bu adrese gelen kişi Dashboard'a yönlendirilir.
        // (Visual Studio'da AdminLayout/Index.cshtml üzerinde Ctrl+Shift+W yapınca da panel açılır.)
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}