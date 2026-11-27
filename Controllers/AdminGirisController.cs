using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Dtos.HesapDtos;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Controllers
{
    // Bu controller'da [Authorize] yok, çünkü giriş yapmamış kişinin de görmesi gerekiyor
    public class AdminGirisController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminGirisController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
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
            return View(new LoginDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto loginDto, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(loginDto);

            var kullanici = await _userManager.FindByEmailAsync(loginDto.Email);

            // Kullanıcı yoksa ya da Admin değilse aynı mesaj (hangi hesapların var olduğu belli olmasın)
            if (kullanici == null || !await _userManager.IsInRoleAsync(kullanici, Roller.Admin))
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı ya da bu hesabın yönetici yetkisi yok.");
                return View(loginDto);
            }

            var sonuc = await _signInManager.PasswordSignInAsync(kullanici, loginDto.Sifre, loginDto.BeniHatirla, lockoutOnFailure: false);
            if (!sonuc.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı ya da bu hesabın yönetici yetkisi yok.");
                return View(loginDto);
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