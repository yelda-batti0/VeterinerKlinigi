using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Dtos.RandevuDtos
{
    public class DeleteRandevuDto
    {
        public int RandevuId { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public string EvcilHayvanAd { get; set; } = string.Empty;
        public string HizmetAd { get; set; } = string.Empty;
        public string VeterinerAdSoyad { get; set; } = string.Empty;
        public RandevuDurum Durum { get; set; }
    }
}