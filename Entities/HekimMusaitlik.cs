using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class HekimMusaitlik
    {
        public int Id { get; set; }

        [Required]
        public int VeterinerHekimId { get; set; }
        public VeterinerHekim? VeterinerHekim { get; set; }

        [Required(ErrorMessage = "Gün seçimi zorunludur.")]
        [Display(Name = "Gün")]
        public DayOfWeek Gun { get; set; }

        [Required(ErrorMessage = "Başlangıç saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Başlangıç")]
        public TimeSpan BaslangicSaati { get; set; }

        [Required(ErrorMessage = "Bitiş saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Bitiş")]
        public TimeSpan BitisSaati { get; set; }
    }
}