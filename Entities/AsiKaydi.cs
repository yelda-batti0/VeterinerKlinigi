namespace VeterinerKlinigi.Entities
{
    public class AsiKaydi
    {
        public int AsiKaydiId { get; set; }
        public string AsiAdi { get; set; } = string.Empty;
        public DateTime YapilmaTarihi { get; set; }
        public DateTime? SonrakiDozTarihi { get; set; }

        public int EvcilHayvanId { get; set; }
        public EvcilHayvan? EvcilHayvan { get; set; }
    }
}