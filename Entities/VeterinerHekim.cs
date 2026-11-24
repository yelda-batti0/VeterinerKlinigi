using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class VeterinerHekim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50)]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50)]
        public string Soyad { get; set; } = string.Empty;

        [Display(Name = "Ad Soyad")]
        public string AdSoyad => $"Vet. Hek. {Ad} {Soyad}";

        [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
        [Display(Name = "Klinik")]
        public int KlinikId { get; set; }
        public Klinik? Klinik { get; set; }

        // Veterinerin giriş yaptığı kullanıcı hesabı
        public string? KullaniciId { get; set; }
        public ApplicationUser? Kullanici { get; set; }

        public ICollection<UzmanlikAlani> UzmanlikAlanlari { get; set; } = new List<UzmanlikAlani>();
        public ICollection<Hizmet> Hizmetler { get; set; } = new List<Hizmet>();
        public ICollection<HekimMusaitlik> Musaitlikler { get; set; } = new List<HekimMusaitlik>();
        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}