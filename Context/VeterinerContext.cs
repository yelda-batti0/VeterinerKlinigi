using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Context
{
    public class VeterinerContext : IdentityDbContext<AppUser>
    {
        public VeterinerContext(DbContextOptions<VeterinerContext> options) : base(options)
        {
        }

        public DbSet<Klinik> Klinikler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }
        public DbSet<Veteriner> Veterinerler { get; set; }
        public DbSet<Musaitlik> Musaitlikler { get; set; }
        public DbSet<EvcilHayvan> EvcilHayvanlar { get; set; }
        public DbSet<AsiKaydi> AsiKayitlari { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Para ve kilo alanlarının ondalık hassasiyeti
            builder.Entity<Hizmet>().Property(h => h.Ucret).HasPrecision(10, 2);
            builder.Entity<Randevu>().Property(r => r.Ucret).HasPrecision(10, 2);
            builder.Entity<EvcilHayvan>().Property(e => e.Kilo).HasPrecision(6, 2);

            // Hizmeti veya veterineri olan klinik silinemesin
            builder.Entity<Hizmet>()
                .HasOne(h => h.Klinik).WithMany(k => k.Hizmetler)
                .HasForeignKey(h => h.KlinikId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Veteriner>()
                .HasOne(v => v.Klinik).WithMany(k => k.Veterinerler)
                .HasForeignKey(v => v.KlinikId).OnDelete(DeleteBehavior.Restrict);

            // Veterinerin giriş hesabı silinirse veteriner kaydı kalır, bağlantı boşalır
            builder.Entity<Veteriner>()
                .HasOne(v => v.AppUser).WithMany()
                .HasForeignKey(v => v.AppUserId).OnDelete(DeleteBehavior.SetNull);

            // Çoka-çok ilişkiler (ara tabloları EF otomatik oluşturur)
            builder.Entity<Veteriner>()
                .HasMany(v => v.Hizmetler).WithMany(h => h.Veterinerler)
                .UsingEntity(j => j.ToTable("VeterinerHizmetleri"));

            builder.Entity<Veteriner>()
                .HasMany(v => v.UzmanlikAlanlari).WithMany(u => u.Veterinerler)
                .UsingEntity(j => j.ToTable("VeterinerUzmanliklari"));

            // Evcil hayvan - sahibi
            builder.Entity<EvcilHayvan>()
                .HasOne(e => e.AppUser).WithMany(u => u.EvcilHayvanlar)
                .HasForeignKey(e => e.AppUserId).OnDelete(DeleteBehavior.Cascade);

            // Randevu ilişkileri: geçmiş randevusu olan hayvan/hizmet/veteriner yanlışlıkla silinemesin
            builder.Entity<Randevu>()
                .HasOne(r => r.EvcilHayvan).WithMany(e => e.Randevular)
                .HasForeignKey(r => r.EvcilHayvanId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Randevu>()
                .HasOne(r => r.Hizmet).WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HizmetId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Randevu>()
                .HasOne(r => r.Veteriner).WithMany(v => v.Randevular)
                .HasForeignKey(r => r.VeterinerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}