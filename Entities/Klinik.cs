using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class Klinik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Klinik adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Klinik Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(250)]
        public string Adres { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20)]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "Açılış saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Açılış Saati")]
        public TimeSpan AcilisSaati { get; set; }

        [Required(ErrorMessage = "Kapanış saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Kapanış Saati")]
        public TimeSpan KapanisSaati { get; set; }

        public ICollection<Hizmet> Hizmetler { get; set; } = new List<Hizmet>();
        public ICollection<VeterinerHekim> Veterinerler { get; set; } = new List<VeterinerHekim>();
    }
}