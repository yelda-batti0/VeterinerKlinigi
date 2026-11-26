using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.KlinikDtos
{
    public class UpdateKlinikDto
    {
        public int KlinikId { get; set; }

        [Required(ErrorMessage = "Klinik adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Klinik adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Klinik Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(250, ErrorMessage = "Adres en fazla 250 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Adres { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20)]
        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "Açılış saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Açılış Saati")]
        public TimeSpan AcilisSaati { get; set; }

        [Required(ErrorMessage = "Kapanış saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Kapanış Saati")]
        public TimeSpan KapanisSaati { get; set; }
    }
}