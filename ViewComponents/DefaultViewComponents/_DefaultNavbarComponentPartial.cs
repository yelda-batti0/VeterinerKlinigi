using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _DefaultNavbarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser? kullanici = null;
            if (UserClaimsPrincipal.Identity?.IsAuthenticated == true)
                kullanici = await _userManager.GetUserAsync(UserClaimsPrincipal);

            return View(kullanici);
        }
    }
}