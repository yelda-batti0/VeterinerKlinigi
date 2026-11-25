using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Models;
using VeterinerKlinigi.Services;

namespace VeterinerKlinigi.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class VeterinerController : Controller
    {
        private readonly IVeterinerService _veterinerService;
        private readonly IKlinikService _klinikService;
        private readonly IHizmetService _hizmetService;
        private readonly IUzmanlikAlaniService _uzmanlikService;

        public VeterinerController(
            IVeterinerService veterinerService,
            IKlinikService klinikService,
            IHizmetService hizmetService,
            IUzmanlikAlaniService uzmanlikService)
        {
            _veterinerService = veterinerService;
            _klinikService = klinikService;
            _hizmetService = hizmetService;
            _uzmanlikService = uzmanlikService;
        }

        public async Task<IActionResult> Index(int? klinikId)
        {
            var veterinerler = await _veterinerService.GetAllAsync();
            var bugun = DateTime.Today.DayOfWeek;

            var model = new VeterinerIndexViewModel
            {
                Veterinerler = veterinerler,
                Klinikler = await _klinikService.GetAllAsync(),
                SeciliKlinikId = klinikId,
                ToplamVeteriner = veterinerler.Count,
                BugunCalisan = veterinerler.Count(v => v.Musaitlikler.Any(m => m.Gun == bugun)),
                MusaitlikGirilmemis = veterinerler.Count(v => !v.Musaitlikler.Any()),
                KlinikSayisi = veterinerler.Select(v => v.KlinikId).Distinct().Count()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var veteriner = await _veterinerService.GetByIdAsync(id);
            if (veteriner == null)
                return NotFound();

            return View(veteriner);
        }

        public async Task<IActionResult> Create(int? klinikId)
        {
            await FormVerileriniYukle(klinikId);
            return View(new VeterinerFormViewModel { KlinikId = klinikId ?? 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeterinerFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Sifre))
                ModelState.AddModelError(nameof(model.Sifre), "Şifre zorunludur.");

            if (!ModelState.IsValid)
            {
                await FormVerileriniYukle(model.KlinikId);
                return View(model);
            }

            var veteriner = new VeterinerHekim
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                KlinikId = model.KlinikId
            };

            var hata = await _veterinerService.CreateAsync(veteriner, model.SeciliUzmanlikIds, model.SeciliHizmetIds, model.Email, model.Sifre!);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await FormVerileriniYukle(model.KlinikId);
                return View(model);
            }

            TempData["Basari"] = "Veteriner eklendi. Şimdi haftalık müsaitlik saatlerini girebilirsiniz.";
            return RedirectToAction(nameof(Musaitlik), new { id = veteriner.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var veteriner = await _veterinerService.GetByIdAsync(id);
            if (veteriner == null)
                return NotFound();

            var model = new VeterinerFormViewModel
            {
                Id = veteriner.Id,
                Ad = veteriner.Ad,
                Soyad = veteriner.Soyad,
                KlinikId = veteriner.KlinikId,
                Email = veteriner.Kullanici?.Email ?? string.Empty,
                SeciliUzmanlikIds = veteriner.UzmanlikAlanlari.Select(u => u.Id).ToList(),
                SeciliHizmetIds = veteriner.Hizmetler.Select(h => h.Id).ToList()
            };

            await FormVerileriniYukle(model.KlinikId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeterinerFormViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await FormVerileriniYukle(model.KlinikId);
                return View(model);
            }

            var veteriner = new VeterinerHekim
            {
                Id = model.Id,
                Ad = model.Ad,
                Soyad = model.Soyad,
                KlinikId = model.KlinikId
            };

            var hata = await _veterinerService.UpdateAsync(veteriner, model.SeciliUzmanlikIds, model.SeciliHizmetIds);
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await FormVerileriniYukle(model.KlinikId);
                return View(model);
            }

            TempData["Basari"] = "Veteriner güncellendi.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var veteriner = await _veterinerService.GetByIdAsync(id);
            if (veteriner == null)
                return NotFound();

            return View(veteriner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hata = await _veterinerService.DeleteAsync(id);
            if (hata != null)
                TempData["Hata"] = hata;
            else
                TempData["Basari"] = "Veteriner ve giriş hesabı silindi.";

            return RedirectToAction(nameof(Index));
        }

        // ---------- MÜSAİTLİK ----------

        public async Task<IActionResult> Musaitlik(int id)
        {
            var veteriner = await _veterinerService.GetByIdAsync(id);
            if (veteriner == null)
                return NotFound();

            return View(veteriner);
        }

        // Birden fazla gün için aynı saat aralığını tek seferde ekler.
        // Kurallar (klinik saatleri, çakışma) her gün için serviste ayrı ayrı kontrol edilir.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MusaitlikEkle(int veterinerHekimId, List<DayOfWeek> gunler, TimeSpan baslangicSaati, TimeSpan bitisSaati)
        {
            if (!gunler.Any())
            {
                TempData["Hata"] = "En az bir gün seçmelisiniz.";
                return RedirectToAction(nameof(Musaitlik), new { id = veterinerHekimId });
            }

            var hatalar = new List<string>();
            int eklenen = 0;

            foreach (var gun in gunler.Distinct())
            {
                var hata = await _veterinerService.MusaitlikEkleAsync(new HekimMusaitlik
                {
                    VeterinerHekimId = veterinerHekimId,
                    Gun = gun,
                    BaslangicSaati = baslangicSaati,
                    BitisSaati = bitisSaati
                });

                if (hata != null)
                    hatalar.Add($"{GunHelper.TurkceAd(gun)}: {hata}");
                else
                    eklenen++;
            }

            if (eklenen > 0)
                TempData["Basari"] = $"{eklenen} gün için müsaitlik eklendi.";
            if (hatalar.Any())
                TempData["Hata"] = string.Join(" ", hatalar);

            return RedirectToAction(nameof(Musaitlik), new { id = veterinerHekimId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MusaitlikSil(int id, int veterinerId)
        {
            var hata = await _veterinerService.MusaitlikSilAsync(id);
            if (hata != null)
                TempData["Hata"] = hata;
            else
                TempData["Basari"] = "Müsaitlik aralığı silindi.";

            return RedirectToAction(nameof(Musaitlik), new { id = veterinerId });
        }

        // Formdaki klinik listesi, uzmanlık ve hizmet kartları için veriler
        private async Task FormVerileriniYukle(int? seciliKlinikId)
        {
            ViewBag.Klinikler = new SelectList(await _klinikService.GetAllAsync(), "Id", "Ad", seciliKlinikId);
            ViewBag.Uzmanliklar = await _uzmanlikService.GetAllAsync();
            ViewBag.Hizmetler = await _hizmetService.GetAllAsync();
        }
    }
}