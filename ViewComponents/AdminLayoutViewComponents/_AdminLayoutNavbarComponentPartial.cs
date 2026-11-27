using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutNavbarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _AdminLayoutNavbarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kullanici = await _userManager.GetUserAsync(UserClaimsPrincipal);
            return View(kullanici);
        }
    }
}