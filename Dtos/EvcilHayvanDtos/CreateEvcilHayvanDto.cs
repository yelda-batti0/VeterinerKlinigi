using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.EvcilHayvanDtos
{
    public class CreateEvcilHayvanDto
    {
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
        public DateTime DogumTarihi { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Kilo zorunludur.")]
        [Range(0.01, 200, ErrorMessage = "Kilo 0,01 ile 200 kg arasında olmalıdır.")]
        [Display(Name = "Kilo (kg)")]
        public decimal Kilo { get; set; }

        // Fotoğraf dosyası (isteğe bağlı). Controller bunu wwwroot'a kaydedip yolunu entity'ye yazacak.
        [Display(Name = "Fotoğraf")]
        public IFormFile? Fotograf { get; set; }
    }
}