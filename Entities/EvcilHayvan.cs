using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class EvcilHayvan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hayvanın adı zorunludur.")]
        [StringLength(50)]
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
        [Range(0.01, 200, ErrorMessage = "Kilo 0.01 ile 200 kg arasında olmalıdır.")]
        [Display(Name = "Kilo (kg)")]
        public decimal Kilo { get; set; }

        [Display(Name = "Fotoğraf")]
        public string? FotografYolu { get; set; }

        // Hayvan sahibi
        public string SahipId { get; set; } = string.Empty;
        public ApplicationUser? Sahip { get; set; }

        public ICollection<AsiKaydi> AsiKayitlari { get; set; } = new List<AsiKaydi>();
        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}