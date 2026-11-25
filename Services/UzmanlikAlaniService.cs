using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public class UzmanlikAlaniService : IUzmanlikAlaniService
    {
        private readonly ApplicationDbContext _context;

        public UzmanlikAlaniService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string?> CreateAsync(UzmanlikAlani uzmanlik)
        {
            if (await AyniIsimVarMi(uzmanlik.Ad, uzmanlik.Id))
                return "Bu isimde bir uzmanlık alanı zaten var.";

            _context.UzmanlikAlanlari.Add(uzmanlik);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var uzmanlik = await _context.UzmanlikAlanlari
               .Include(u => u.Veterinerler)
               .FirstOrDefaultAsync(u => u.Id == id);

            if (uzmanlik == null)
                return "Uzmanlık alanı bulunamadı.";

            if (uzmanlik.Veterinerler.Any())
                return "Bu uzmanlık alanına sahip veterinerler olduğu için silinemez.";

            _context.UzmanlikAlanlari.Remove(uzmanlik);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<List<UzmanlikAlani>> GetAllAsync()
        {
            return await _context.UzmanlikAlanlari
                .Include(u => u.Veterinerler)
                    .ThenInclude(v => v.Klinik)
                .AsSplitQuery()
                .OrderBy(u => u.Ad)
                .ToListAsync();
        }
        public async Task<UzmanlikAlani?> GetByIdAsync(int id)
        {
            return await _context.UzmanlikAlanlari
                .Include(u => u.Veterinerler)
                    .ThenInclude(v => v.Klinik)
                .Include(u => u.Veterinerler)
                    .ThenInclude(v => v.UzmanlikAlanlari)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string?> UpdateAsync(UzmanlikAlani uzmanlik)
        {
            if (await AyniIsimVarMi(uzmanlik.Ad, uzmanlik.Id))
                return "Bu isimde bir uzmanlık alanı zaten var.";

            _context.UzmanlikAlanlari.Update(uzmanlik);
            await _context.SaveChangesAsync();
            return null; ;
        }

        private async Task<bool> AyniIsimVarMi(string ad, int haricId)
        {
            return await _context.UzmanlikAlanlari
                .AnyAsync(u => u.Ad == ad.Trim() && u.Id != haricId);
        }
    }
}
