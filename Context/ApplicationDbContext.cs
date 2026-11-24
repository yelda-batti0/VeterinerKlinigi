using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Klinik> Klinikler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }
        public DbSet<VeterinerHekim> VeterinerHekimler { get; set; }
        public DbSet<HekimMusaitlik> HekimMusaitlikleri { get; set; }
        public DbSet<EvcilHayvan> EvcilHayvanlar { get; set; }
        public DbSet<AsiKaydi> AsiKayitlari { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Para ve kilo alanları için ondalık hassasiyet
            builder.Entity<Hizmet>().Property(h => h.Ucret).HasPrecision(10, 2);
            builder.Entity<Randevu>().Property(r => r.Ucret).HasPrecision(10, 2);
            builder.Entity<EvcilHayvan>().Property(e => e.Kilo).HasPrecision(6, 2);

            // İçinde hizmet/hekim olan klinik silinemesin
            builder.Entity<Hizmet>()
                .HasOne(h => h.Klinik).WithMany(k => k.Hizmetler)
                .HasForeignKey(h => h.KlinikId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VeterinerHekim>()
                .HasOne(v => v.Klinik).WithMany(k => k.Veterinerler)
                .HasForeignKey(v => v.KlinikId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VeterinerHekim>()
                .HasOne(v => v.Kullanici).WithMany()
                .HasForeignKey(v => v.KullaniciId).OnDelete(DeleteBehavior.SetNull);

            // Çoktan-çoğa ilişkiler (ara tabloları EF otomatik oluşturur)
            builder.Entity<VeterinerHekim>()
                .HasMany(v => v.Hizmetler).WithMany(h => h.Veterinerler)
                .UsingEntity(j => j.ToTable("VeterinerHizmetleri"));

            builder.Entity<VeterinerHekim>()
                .HasMany(v => v.UzmanlikAlanlari).WithMany(u => u.Veterinerler)
                .UsingEntity(j => j.ToTable("VeterinerUzmanliklari"));

            // Evcil hayvan - sahip
            builder.Entity<EvcilHayvan>()
                .HasOne(e => e.Sahip).WithMany(u => u.EvcilHayvanlar)
                .HasForeignKey(e => e.SahipId).OnDelete(DeleteBehavior.Cascade);

            // Randevu ilişkileri: geçmiş randevusu olan hizmet/hekim/hayvan yanlışlıkla silinmesin
            builder.Entity<Randevu>()
                .HasOne(r => r.EvcilHayvan).WithMany(e => e.Randevular)
                .HasForeignKey(r => r.EvcilHayvanId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Randevu>()
                .HasOne(r => r.Hizmet).WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HizmetId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Randevu>()
                .HasOne(r => r.VeterinerHekim).WithMany(v => v.Randevular)
                .HasForeignKey(r => r.VeterinerHekimId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}