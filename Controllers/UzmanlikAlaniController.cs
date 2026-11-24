using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
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
            return View(await _uzmanlikService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new UzmanlikAlani());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UzmanlikAlani uzmanlik)
        {
            if (!ModelState.IsValid)
                return View(uzmanlik);

            var hata = await _uzmanlikService.CreateAsync(uzmanlik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
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

            return View(uzmanlik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UzmanlikAlani uzmanlik)
        {
            if (id != uzmanlik.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(uzmanlik);

            var hata = await _uzmanlikService.UpdateAsync(uzmanlik);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
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
    }
}