using System.ComponentModel.DataAnnotations;

namespace VeterinerKlinigi.Entities
{
    public class AsiKaydi
    {
        public int Id { get; set; }

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

        [Required(ErrorMessage = "Evcil hayvan seçimi zorunludur.")]
        [Display(Name = "Evcil Hayvan")]
        public int EvcilHayvanId { get; set; }
        public EvcilHayvan? EvcilHayvan { get; set; }
    }
}