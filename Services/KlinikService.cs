using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public class KlinikService : IKlinikService
    {
        private readonly ApplicationDbContext _context;

        public KlinikService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string?> CreateAsync(Klinik klinik)
        {
            var hata = Dogrula(klinik);
            if (hata != null) return hata;

            _context.Klinikler.Add(klinik);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var klinik = await _context.Klinikler.FindAsync(id);
            if (klinik == null)
                return "Klinik bulunamadı.";

            bool bagliKayitVar = await _context.Hizmetler.AnyAsync(h => h.KlinikId == id)
                              || await _context.VeterinerHekimler.AnyAsync(v => v.KlinikId == id);
            if (bagliKayitVar)
                return "Bu kliniğe bağlı hizmet veya veteriner olduğu için silinemez. Önce onları silin.";

            _context.Klinikler.Remove(klinik);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<List<Klinik>> GetAllAsync()
        {
            return await _context.Klinikler
                .Include(k => k.Hizmetler)
                .Include(k => k.Veterinerler)
                .OrderBy(k => k.Ad)
                .ToListAsync();

        }

        public async Task<Klinik?> GetByIdAsync(int id)
        {
            return await _context.Klinikler
                .Include(k => k.Hizmetler)
                .Include(k => k.Veterinerler)
                    .ThenInclude(v => v.UzmanlikAlanlari)
                .AsSplitQuery()
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<string?> UpdateAsync(Klinik klinik)
        {
            var hata = Dogrula(klinik);
            if (hata != null) return hata;

            _context.Klinikler.Update(klinik);
            await _context.SaveChangesAsync();
            return null;
        }

        private static string? Dogrula(Klinik klinik)
        {
            if (klinik.KapanisSaati <= klinik.AcilisSaati)
                return "Kapanış saati, açılış saatinden sonra olmalıdır.";
            return null;
        }
    }
}
