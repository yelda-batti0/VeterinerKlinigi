using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.MusaitlikDtos
{
    public class CreateMusaitlikDto
    {
        public int VeterinerId { get; set; }

        [Required(ErrorMessage = "En az bir gün seçmelisiniz.")]
        [MinLength(1, ErrorMessage = "En az bir gün seçmelisiniz.")]
        [Display(Name = "Günler")]
        public List<DayOfWeek> Gunler { get; set; } = new();

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