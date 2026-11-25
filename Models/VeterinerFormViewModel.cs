using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Models
{
    public class VeterinerFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50)]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50)]
        public string Soyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Klinik seçimi zorunludur.")]
        [Display(Name = "Klinik")]
        public int KlinikId { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta (giriş için)")]
        public string Email { get; set; } = string.Empty;

        // Sadece yeni veteriner eklerken zorunlu
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Şifre en az 3 karakter olmalıdır.")]
        [Display(Name = "Şifre")]
        public string? Sifre { get; set; }

        public List<int> SeciliUzmanlikIds { get; set; } = new();
        public List<int> SeciliHizmetIds { get; set; } = new();
    }
}