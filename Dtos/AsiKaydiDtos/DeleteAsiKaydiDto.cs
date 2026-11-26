namespace VeterinerKlinigi.Dtos.AsiKaydiDtos
{
    public class DeleteAsiKaydiDto
    {
        public int AsiKaydiId { get; set; }
        public int EvcilHayvanId { get; set; }
        public string EvcilHayvanAd { get; set; } = string.Empty;
        public string AsiAdi { get; set; } = string.Empty;
        public DateTime YapilmaTarihi { get; set; }
    }
}