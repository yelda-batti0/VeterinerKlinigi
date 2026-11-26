using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;

namespace VeterinerKlinigi.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roller.Admin)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}