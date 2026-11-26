using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.UzmanlikAlaniDtos
{
    public class UpdateUzmanlikAlaniDto
    {
        public int UzmanlikAlaniId { get; set; }

        [Required(ErrorMessage = "Uzmanlık adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Uzmanlık adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Uzmanlık Alanı")]
        public string Ad { get; set; } = string.Empty;
    }
}