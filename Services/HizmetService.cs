using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public class HizmetService : IHizmetService
    {
        private readonly ApplicationDbContext _context;

        public HizmetService(ApplicationDbContext context)
        {
            _context = context;
        }

        // klinikId verilirse sadece o kliniğin hizmetleri gelir
        public async Task<List<Hizmet>> GetAllAsync(int? klinikId = null)
        {
            var sorgu = _context.Hizmetler
                .Include(h => h.Klinik)
                .Include(h => h.Veterinerler)
                .Include(h => h.Randevular)
                .AsSplitQuery()
                .AsQueryable();

            if (klinikId.HasValue)
                sorgu = sorgu.Where(h => h.KlinikId == klinikId.Value);

            return await sorgu
                .OrderBy(h => h.Klinik!.Ad)
                .ThenBy(h => h.Ad)
                .ToListAsync();
        }

        public async Task<Hizmet?> GetByIdAsync(int id)
        {
            return await _context.Hizmetler
                .Include(h => h.Klinik)
                .Include(h => h.Veterinerler)
                    .ThenInclude(v => v.UzmanlikAlanlari)
                .Include(h => h.Randevular)
                .AsSplitQuery()
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<string?> CreateAsync(Hizmet hizmet)
        {
            var hata = await Dogrula(hizmet);
            if (hata != null) return hata;

            _context.Hizmetler.Add(hizmet);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> UpdateAsync(Hizmet hizmet)
        {
            var hata = await Dogrula(hizmet);
            if (hata != null) return hata;

            _context.Hizmetler.Update(hizmet);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var hizmet = await _context.Hizmetler.FindAsync(id);
            if (hizmet == null)
                return "Hizmet bulunamadı.";

            if (await _context.Randevular.AnyAsync(r => r.HizmetId == id))
                return "Bu hizmete ait randevular olduğu için silinemez.";

            _context.Hizmetler.Remove(hizmet);
            await _context.SaveChangesAsync();
            return null;
        }

        private async Task<string?> Dogrula(Hizmet hizmet)
        {
            if (!await _context.Klinikler.AnyAsync(k => k.Id == hizmet.KlinikId))
                return "Seçilen klinik bulunamadı.";

            bool ayniIsim = await _context.Hizmetler.AnyAsync(h =>
                h.KlinikId == hizmet.KlinikId &&
                h.Ad == hizmet.Ad.Trim() &&
                h.Id != hizmet.Id);

            if (ayniIsim)
                return "Bu klinikte aynı isimde bir hizmet zaten var.";

            return null;
        }
    }
}