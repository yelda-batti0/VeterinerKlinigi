using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Controllers
{
    // Bu controller'da [Authorize] YOK, çünkü giriş yapmamış kişinin de görmesi gerekiyor
    public class AdminGirisController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminGirisController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string? returnUrl = null)
        {
            if (User.IsInRole(Roller.Admin))
                return RedirectToAction("Index", "Dashboard");

            ViewBag.ReturnUrl = returnUrl;
            return View(new AdminGirisViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AdminGirisViewModel model, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var kullanici = await _userManager.FindByEmailAsync(model.Email);

            // Kullanıcı yoksa veya Admin rolünde değilse aynı mesajı veriyoruz (hangi hesapların var olduğu belli olmasın)
            if (kullanici == null || !await _userManager.IsInRoleAsync(kullanici, Roller.Admin))
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı ya da bu hesabın yönetici yetkisi yok.");
                return View(model);
            }

            var sonuc = await _signInManager.PasswordSignInAsync(kullanici, model.Sifre, model.BeniHatirla, lockoutOnFailure: false);
            if (!sonuc.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı ya da bu hesabın yönetici yetkisi yok.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cikis()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}