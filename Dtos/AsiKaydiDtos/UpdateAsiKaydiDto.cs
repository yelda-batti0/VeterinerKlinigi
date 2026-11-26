using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Dtos.AsiKaydiDtos
{
    public class UpdateAsiKaydiDto
    {
        public int AsiKaydiId { get; set; }
        public int EvcilHayvanId { get; set; }

        [Required(ErrorMessage = "Aşı adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Aşı Adı")]
        public string AsiAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yapılma tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Yapıldığı Tarih")]
        public DateTime YapilmaTarihi { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Sonraki Doz Tarihi")]
        public DateTime? SonrakiDozTarihi { get; set; }
    }
}