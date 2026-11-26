using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.EvcilHayvanDtos
{
    public class UpdateEvcilHayvanDto
    {
        public int EvcilHayvanId { get; set; }

        [Required(ErrorMessage = "Hayvanın adı zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tür zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Tür")]
        public string Tur { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Irk")]
        public string? Irk { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Kilo zorunludur.")]
        [Range(0.01, 200, ErrorMessage = "Kilo 0,01 ile 200 kg arasında olmalıdır.")]
        [Display(Name = "Kilo (kg)")]
        public decimal Kilo { get; set; }

        // Mevcut fotoğraf (sayfada göstermek için)
        public string? FotografUrl { get; set; }

        // Yeni fotoğraf seçilirse eskisinin yerine geçer
        [Display(Name = "Yeni Fotoğraf")]
        public IFormFile? Fotograf { get; set; }
    }
}