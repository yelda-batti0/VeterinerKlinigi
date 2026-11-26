using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.HizmetDtos
{
    public class CreateHizmetDto
    {
        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Hizmet adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Hizmet Adı")]
        public string Ad { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Süre zorunludur.")]
        [Range(5, 480, ErrorMessage = "Süre 5 ile 480 dakika arasında olmalıdır.")]
        [Display(Name = "Süre (dk)")]
        public int SureDakika { get; set; } = 30;

        [Required(ErrorMessage = "Ücret zorunludur.")]
        [Range(0, 100000, ErrorMessage = "Ücret 0 ile 100.000 arasında olmalıdır.")]
        [Display(Name = "Ücret (₺)")]
        public decimal Ucret { get; set; }

        [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
        [Display(Name = "Klinik")]
        public int KlinikId { get; set; }
    }
}