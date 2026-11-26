using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.VeterinerDtos
{
    public class UpdateVeterinerDto
    {
        public int VeterinerId { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Ad")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
        [Display(Name = "Klinik")]
        public int KlinikId { get; set; }

        [Display(Name = "E-posta (giriş için)")]
        public string? Email { get; set; }

        public List<int> SeciliUzmanlikIds { get; set; } = new();
        public List<int> SeciliHizmetIds { get; set; } = new();
    }
}