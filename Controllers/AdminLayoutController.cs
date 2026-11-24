using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class AdminLayoutController : Controller
    {
      
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}