using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Services
{
    public class VeterinerService : IVeterinerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VeterinerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<VeterinerHekim>> GetAllAsync(int? klinikId = null)
        {
            var sorgu = _context.VeterinerHekimler
                .Include(v => v.Klinik)
                .Include(v => v.UzmanlikAlanlari)
                .Include(v => v.Hizmetler)
                .Include(v => v.Musaitlikler)
                .Include(v => v.Kullanici)
                .Include(v => v.Randevular)
                .AsSplitQuery()
                .AsQueryable();

            if (klinikId.HasValue)
                sorgu = sorgu.Where(v => v.KlinikId == klinikId.Value);

            return await sorgu
                .OrderBy(v => v.Ad)
                .ThenBy(v => v.Soyad)
                .ToListAsync();
        }

        public async Task<VeterinerHekim?> GetByIdAsync(int id)
        {
            return await _context.VeterinerHekimler
                .Include(v => v.Klinik)
                .Include(v => v.UzmanlikAlanlari)
                .Include(v => v.Hizmetler)
                .Include(v => v.Musaitlikler)
                .Include(v => v.Kullanici)
                .Include(v => v.Randevular)
                .AsSplitQuery()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<string?> CreateAsync(VeterinerHekim veteriner, List<int> uzmanlikIds, List<int> hizmetIds, string email, string sifre)
        {
            var hata = await Dogrula(veteriner.KlinikId, hizmetIds);
            if (hata != null) return hata;

            if (await _userManager.FindByEmailAsync(email) != null)
                return "Bu e-posta adresiyle kayıtlı bir kullanıcı zaten var.";

            // Kullanıcı hesabı ve veteriner kaydı birlikte oluşsun, biri hata verirse ikisi de geri alınsın
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var kullanici = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                Ad = veteriner.Ad,
                Soyad = veteriner.Soyad
            };

            var sonuc = await _userManager.CreateAsync(kullanici, sifre);
            if (!sonuc.Succeeded)
                return string.Join(" ", sonuc.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(kullanici, Roller.Veteriner);

            veteriner.KullaniciId = kullanici.Id;
            veteriner.UzmanlikAlanlari = await _context.UzmanlikAlanlari.Where(u => uzmanlikIds.Contains(u.Id)).ToListAsync();
            veteriner.Hizmetler = await _context.Hizmetler.Where(h => hizmetIds.Contains(h.Id)).ToListAsync();

            _context.VeterinerHekimler.Add(veteriner);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return null;
        }

        public async Task<string?> UpdateAsync(VeterinerHekim guncel, List<int> uzmanlikIds, List<int> hizmetIds)
        {
            var veteriner = await _context.VeterinerHekimler
                .Include(v => v.UzmanlikAlanlari)
                .Include(v => v.Hizmetler)
                .Include(v => v.Kullanici)
                .FirstOrDefaultAsync(v => v.Id == guncel.Id);

            if (veteriner == null)
                return "Veteriner bulunamadı.";

            var hata = await Dogrula(guncel.KlinikId, hizmetIds);
            if (hata != null) return hata;

            veteriner.Ad = guncel.Ad;
            veteriner.Soyad = guncel.Soyad;
            veteriner.KlinikId = guncel.KlinikId;

            // Çoktan-çoğa ilişkileri yeniden kur
            veteriner.UzmanlikAlanlari.Clear();
            foreach (var u in await _context.UzmanlikAlanlari.Where(u => uzmanlikIds.Contains(u.Id)).ToListAsync())
                veteriner.UzmanlikAlanlari.Add(u);

            veteriner.Hizmetler.Clear();
            foreach (var h in await _context.Hizmetler.Where(h => hizmetIds.Contains(h.Id)).ToListAsync())
                veteriner.Hizmetler.Add(h);

            // Giriş hesabındaki ad-soyad da güncel kalsın
            if (veteriner.Kullanici != null)
            {
                veteriner.Kullanici.Ad = guncel.Ad;
                veteriner.Kullanici.Soyad = guncel.Soyad;
            }

            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var veteriner = await _context.VeterinerHekimler.FindAsync(id);
            if (veteriner == null)
                return "Veteriner bulunamadı.";

            if (await _context.Randevular.AnyAsync(r => r.VeterinerHekimId == id))
                return "Bu veterinere ait randevular olduğu için silinemez.";

            var kullaniciId = veteriner.KullaniciId;

            // Müsaitlikler ve ara tablo kayıtları otomatik silinir (cascade)
            _context.VeterinerHekimler.Remove(veteriner);
            await _context.SaveChangesAsync();

            // Veterinerin giriş hesabını da sil
            if (kullaniciId != null)
            {
                var kullanici = await _userManager.FindByIdAsync(kullaniciId);
                if (kullanici != null)
                    await _userManager.DeleteAsync(kullanici);
            }

            return null;
        }

        public async Task<string?> MusaitlikEkleAsync(HekimMusaitlik musaitlik)
        {
            var veteriner = await _context.VeterinerHekimler
                .Include(v => v.Klinik)
                .Include(v => v.Musaitlikler)
                .FirstOrDefaultAsync(v => v.Id == musaitlik.VeterinerHekimId);

            if (veteriner == null)
                return "Veteriner bulunamadı.";

            if (musaitlik.BitisSaati <= musaitlik.BaslangicSaati)
                return "Bitiş saati, başlangıç saatinden sonra olmalıdır.";

            var klinik = veteriner.Klinik!;
            if (musaitlik.BaslangicSaati < klinik.AcilisSaati || musaitlik.BitisSaati > klinik.KapanisSaati)
                return $"Müsaitlik, kliniğin çalışma saatleri ({klinik.AcilisSaati:hh\\:mm} - {klinik.KapanisSaati:hh\\:mm}) içinde olmalıdır.";

            // Aynı gün içinde iki aralık çakışıyor mu? (A.başlangıç < B.bitiş VE B.başlangıç < A.bitiş)
            bool cakisma = veteriner.Musaitlikler.Any(m =>
                m.Gun == musaitlik.Gun &&
                musaitlik.BaslangicSaati < m.BitisSaati &&
                m.BaslangicSaati < musaitlik.BitisSaati);

            if (cakisma)
                return "Bu saatler, aynı gün için girilmiş başka bir müsaitlik aralığıyla çakışıyor.";

            _context.HekimMusaitlikleri.Add(musaitlik);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<string?> MusaitlikSilAsync(int musaitlikId)
        {
            var musaitlik = await _context.HekimMusaitlikleri.FindAsync(musaitlikId);
            if (musaitlik == null)
                return "Müsaitlik kaydı bulunamadı.";

            _context.HekimMusaitlikleri.Remove(musaitlik);
            await _context.SaveChangesAsync();
            return null;
        }

        private async Task<string?> Dogrula(int klinikId, List<int> hizmetIds)
        {
            if (!await _context.Klinikler.AnyAsync(k => k.Id == klinikId))
                return "Seçilen klinik bulunamadı.";

            if (!hizmetIds.Any())
                return "Veterinerin verebileceği en az bir hizmet seçmelisiniz.";

            bool baskaKlinigeAit = await _context.Hizmetler
                .AnyAsync(h => hizmetIds.Contains(h.Id) && h.KlinikId != klinikId);

            if (baskaKlinigeAit)
                return "Seçilen hizmetler, veterinerin çalıştığı kliniğe ait olmalıdır.";

            return null;
        }
    }
}