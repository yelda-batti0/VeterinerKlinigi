using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class UzmanlikAlani
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Uzmanlık adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Uzmanlık Alanı")]
        public string Ad { get; set; } = string.Empty;

        public ICollection<VeterinerHekim> Veterinerler { get; set; } = new List<VeterinerHekim>();
    }
}