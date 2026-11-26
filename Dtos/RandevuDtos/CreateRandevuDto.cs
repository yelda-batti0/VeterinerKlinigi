using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.RandevuDtos
{
    public class CreateRandevuDto
    {
        [Required(ErrorMessage = "Evcil hayvan seçimi zorunludur.")]
        [Display(Name = "Evcil Hayvan")]
        public int EvcilHayvanId { get; set; }

        [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
        [Display(Name = "Hizmet")]
        public int HizmetId { get; set; }

        [Required(ErrorMessage = "Veteriner seçimi zorunludur.")]
        [Display(Name = "Veteriner")]
        public int VeterinerId { get; set; }

        [Required(ErrorMessage = "Tarih zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Saat zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Saat")]
        public TimeSpan Saat { get; set; }
    }
}