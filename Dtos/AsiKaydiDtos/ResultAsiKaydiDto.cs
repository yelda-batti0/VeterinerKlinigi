namespace VeterinerKlinigi.Dtos.AsiKaydiDtos
{
    public class ResultAsiKaydiDto
    {
        public int AsiKaydiId { get; set; }
        public string AsiAdi { get; set; } = string.Empty;
        public DateTime YapilmaTarihi { get; set; }
        public DateTime? SonrakiDozTarihi { get; set; }
        public int EvcilHayvanId { get; set; }
        public string EvcilHayvanAd { get; set; } = string.Empty;
    }
}