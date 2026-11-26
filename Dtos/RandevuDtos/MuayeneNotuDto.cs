using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.RandevuDtos
{
    public class MuayeneNotuDto
    {
        public int RandevuId { get; set; }

        [Required(ErrorMessage = "Muayene notu boş olamaz.")]
        [StringLength(2000, ErrorMessage = "Muayene notu en fazla 2000 karakter olabilir.")]
        [Display(Name = "Muayene Notu")]
        public string MuayeneNotu { get; set; } = string.Empty;
    }
}