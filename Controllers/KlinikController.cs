using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Services;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class KlinikController : Controller
    {
        private readonly IKlinikService _klinikService;

        public KlinikController(IKlinikService klinikService)
        {
            _klinikService = klinikService;
        }

        public async Task<IActionResult> Index()
        {
            var klinikler = await _klinikService.GetAllAsync();
            var acikSayisi = klinikler.Count(KlinikHelper.SuAnAcikMi);

            var model = new KlinikIndexViewModel
            {
                Klinikler = klinikler,
                ToplamKlinik = klinikler.Count,
                AcikKlinik = acikSayisi,
                KapaliKlinik = klinikler.Count - acikSayisi,
                ToplamVeteriner = klinikler.Sum(k => k.Veterinerler.Count)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var klinik = await _klinikService.GetByIdAsync(id);
            if (klinik == null)
                return NotFound();

            return View(klinik);
        }
        public IActionResult Create()
        {
            var klinik = new Klinik
            {
                AcilisSaati = new TimeSpan(9, 0, 0),
                KapanisSaati = new TimeSpan(18, 0, 0)
            };
            return View(klinik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Klinik klinik)
        {
            if (!ModelState.IsValid)
                return View(klinik);

            var hata = await _klinikService.CreateAsync(klinik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                return View(klinik);
            }

            TempData["Basari"] = "Klinik başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var klinik = await _klinikService.GetByIdAsync(id);
            if (klinik == null)
                return NotFound();

            return View(klinik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Klinik klinik)
        {
            if (id != klinik.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(klinik);

            var hata = await _klinikService.UpdateAsync(klinik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                return View(klinik);
            }

            TempData["Basari"] = "Klinik başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var klinik = await _klinikService.GetByIdAsync(id);
            if (klinik == null)
                return NotFound();

            return View(klinik);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hata = await _klinikService.DeleteAsync(id);
            if (hata != null)
                TempData["Hata"] = hata;
            else
                TempData["Basari"] = "Klinik silindi.";

            return RedirectToAction(nameof(Index));
        }
    }
}