using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class Randevu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Evcil hayvan seçimi zorunludur.")]
        [Display(Name = "Evcil Hayvan")]
        public int EvcilHayvanId { get; set; }
        public EvcilHayvan? EvcilHayvan { get; set; }

        [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
        [Display(Name = "Hizmet")]
        public int HizmetId { get; set; }
        public Hizmet? Hizmet { get; set; }

        [Required(ErrorMessage = "Veteriner seçimi zorunludur.")]
        [Display(Name = "Veteriner")]
        public int VeterinerHekimId { get; set; }
        public VeterinerHekim? VeterinerHekim { get; set; }

        [Required]
        [Display(Name = "Başlangıç")]
        public DateTime BaslangicZamani { get; set; }

        [Required]
        [Display(Name = "Bitiş")]
        public DateTime BitisZamani { get; set; }

        // Randevu alındığı andaki hizmet bilgisi (hizmet fiyatı sonradan değişse de randevu kaydı korunur)
        [Display(Name = "Süre (dk)")]
        public int SureDakika { get; set; }

        [Display(Name = "Ücret (₺)")]
        public decimal Ucret { get; set; }

        [Display(Name = "Durum")]
        public RandevuDurum Durum { get; set; } = RandevuDurum.Bekliyor;

        [StringLength(2000)]
        [Display(Name = "Muayene Notu")]
        public string? MuayeneNotu { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
    }
}