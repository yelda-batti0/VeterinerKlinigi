using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Services;
using VeterinerKlinigi.Models;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class HizmetController : Controller
    {
        private readonly IHizmetService _hizmetService;
        private readonly IKlinikService _klinikService;

        public HizmetController(IHizmetService hizmetService, IKlinikService klinikService)
        {
            _hizmetService = hizmetService;
            _klinikService = klinikService;
        }

        public async Task<IActionResult> Index(int? klinikId)
        {
            var hizmetler = await _hizmetService.GetAllAsync();

            var model = new HizmetIndexViewModel
            {
                Hizmetler = hizmetler,
                Klinikler = await _klinikService.GetAllAsync(),
                SeciliKlinikId = klinikId,
                ToplamHizmet = hizmetler.Count,
                OrtalamaUcret = hizmetler.Any() ? hizmetler.Average(h => h.Ucret) : 0,
                OrtalamaSure = hizmetler.Any() ? (int)Math.Round(hizmetler.Average(h => h.SureDakika)) : 0,
                VeterinersizHizmet = hizmetler.Count(h => !h.Veterinerler.Any())
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hizmet = await _hizmetService.GetByIdAsync(id);
            if (hizmet == null)
                return NotFound();

            return View(hizmet);
        }

        public async Task<IActionResult> Create(int? klinikId)
        {
            await KlinikleriYukle(klinikId);
            return View(new Hizmet { KlinikId = klinikId ?? 0, SureDakika = 30 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hizmet hizmet)
        {
            if (!ModelState.IsValid)
            {
                await KlinikleriYukle(hizmet.KlinikId);
                return View(hizmet);
            }

            var hata = await _hizmetService.CreateAsync(hizmet);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await KlinikleriYukle(hizmet.KlinikId);
                return View(hizmet);
            }

            TempData["Basari"] = "Hizmet eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hizmet = await _hizmetService.GetByIdAsync(id);
            if (hizmet == null)
                return NotFound();

            await KlinikleriYukle(hizmet.KlinikId);
            return View(hizmet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hizmet hizmet)
        {
            if (id != hizmet.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await KlinikleriYukle(hizmet.KlinikId);
                return View(hizmet);
            }

            var hata = await _hizmetService.UpdateAsync(hizmet);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await KlinikleriYukle(hizmet.KlinikId);
                return View(hizmet);
            }

            TempData["Basari"] = "Hizmet güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hizmet = await _hizmetService.GetByIdAsync(id);
            if (hizmet == null)
                return NotFound();

            return View(hizmet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hata = await _hizmetService.DeleteAsync(id);
            if (hata != null)
                TempData["Hata"] = hata;
            else
                TempData["Basari"] = "Hizmet silindi.";

            return RedirectToAction(nameof(Index));
        }

        // Klinik açılır listesini doldurur
        private async Task KlinikleriYukle(int? seciliId)
        {
            var klinikler = await _klinikService.GetAllAsync();
            ViewBag.Klinikler = new SelectList(klinikler, "Id", "Ad", seciliId);
        }
    }
}