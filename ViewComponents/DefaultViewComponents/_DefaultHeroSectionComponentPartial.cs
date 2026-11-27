using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;

namespace VeterinerKlinigi.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly VeterinerContext _context;

        public _DefaultHeroSectionComponentPartial(VeterinerContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.KlinikSayisi = await _context.Klinikler.CountAsync();
            ViewBag.VeterinerSayisi = await _context.Veterinerler.CountAsync();
            ViewBag.HizmetSayisi = await _context.Hizmetler.Select(h => h.Ad).Distinct().CountAsync();
            return View();
        }
    }
}