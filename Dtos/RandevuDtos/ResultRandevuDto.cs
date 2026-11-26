using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Dtos.RandevuDtos
{
    public class ResultRandevuDto
    {
        public int RandevuId { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int SureDakika { get; set; }
        public decimal Ucret { get; set; }
        public RandevuDurum Durum { get; set; }
        public string? MuayeneNotu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

        public int EvcilHayvanId { get; set; }
        public string EvcilHayvanAd { get; set; } = string.Empty;
        public string SahipAdSoyad { get; set; } = string.Empty;

        public int HizmetId { get; set; }
        public string HizmetAd { get; set; } = string.Empty;

        public int VeterinerId { get; set; }
        public string VeterinerAdSoyad { get; set; } = string.Empty;
        public string KlinikAd { get; set; } = string.Empty;
    }
}