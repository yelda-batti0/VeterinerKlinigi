using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class Hizmet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Hizmet Adı")]
        public string Ad { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Süre zorunludur.")]
        [Range(5, 480, ErrorMessage = "Süre 5 ile 480 dakika arasında olmalıdır.")]
        [Display(Name = "Süre (dk)")]
        public int SureDakika { get; set; }

        [Required(ErrorMessage = "Ücret zorunludur.")]
        [Range(0, 100000, ErrorMessage = "Ücret 0 ile 100.000 arasında olmalıdır.")]
        [Display(Name = "Ücret (₺)")]
        public decimal Ucret { get; set; }

        [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
        [Display(Name = "Klinik")]
        public int KlinikId { get; set; }
        public Klinik? Klinik { get; set; }

        public ICollection<VeterinerHekim> Veterinerler { get; set; } = new List<VeterinerHekim>();
        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}