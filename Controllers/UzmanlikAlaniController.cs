using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Models;
using VeterinerKlinigi.Services;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class UzmanlikAlaniController : Controller
    {
        private readonly IUzmanlikAlaniService _uzmanlikService;

        public UzmanlikAlaniController(IUzmanlikAlaniService uzmanlikService)
        {
            _uzmanlikService = uzmanlikService;
        }

        public async Task<IActionResult> Index()
        {
            var uzmanliklar = await _uzmanlikService.GetAllAsync();
            var enYaygin = uzmanliklar
                .Where(u => u.Veterinerler.Any())
                .OrderByDescending(u => u.Veterinerler.Count)
                .FirstOrDefault();

            var model = new UzmanlikIndexViewModel
            {
                Uzmanliklar = uzmanliklar,
                ToplamUzmanlik = uzmanliklar.Count,
                AtanmisUzmanlik = uzmanliklar.Count(u => u.Veterinerler.Any()),
                BostaUzmanlik = uzmanliklar.Count(u => !u.Veterinerler.Any()),
                EnYaygin = enYaygin?.Ad ?? "-"
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var uzmanlik = await _uzmanlikService.GetByIdAsync(id);
            if (uzmanlik == null)
                return NotFound();

            return View(uzmanlik);
        }

        public async Task<IActionResult> Create()
        {
            await MevcutAdlariYukle(0);
            return View(new UzmanlikAlani());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UzmanlikAlani uzmanlik)
        {
            if (!ModelState.IsValid)
            {
                await MevcutAdlariYukle(0);
                return View(uzmanlik);
            }

            var hata = await _uzmanlikService.CreateAsync(uzmanlik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await MevcutAdlariYukle(0);
                return View(uzmanlik);
            }

            TempData["Basari"] = "Uzmanlık alanı eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var uzmanlik = await _uzmanlikService.GetByIdAsync(id);
            if (uzmanlik == null)
                return NotFound();

            await MevcutAdlariYukle(id);
            return View(uzmanlik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UzmanlikAlani uzmanlik)
        {
            if (id != uzmanlik.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await MevcutAdlariYukle(id);
                return View(uzmanlik);
            }

            var hata = await _uzmanlikService.UpdateAsync(uzmanlik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await MevcutAdlariYukle(id);
                return View(uzmanlik);
            }

            TempData["Basari"] = "Uzmanlık alanı güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var uzmanlik = await _uzmanlikService.GetByIdAsync(id);
            if (uzmanlik == null)
                return NotFound();

            return View(uzmanlik);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hata = await _uzmanlikService.DeleteAsync(id);
            if (hata != null)
                TempData["Hata"] = hata;
            else
                TempData["Basari"] = "Uzmanlık alanı silindi.";

            return RedirectToAction(nameof(Index));
        }

        // Formda "bu isim zaten var" uyarısı için mevcut isimler (düzenlenen kayıt hariç)
        private async Task MevcutAdlariYukle(int haricId)
        {
            var tumu = await _uzmanlikService.GetAllAsync();
            ViewBag.MevcutAdlar = tumu.Where(u => u.Id != haricId).Select(u => u.Ad).ToList();
        }
    }
}