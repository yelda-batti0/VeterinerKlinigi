namespace VeterinerKlinigi.Entities
{
    public class EvcilHayvan
    {
        public int EvcilHayvanId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Tur { get; set; } = string.Empty;
        public string? Irk { get; set; }
        public DateTime DogumTarihi { get; set; }
        public decimal Kilo { get; set; }
        public string? FotografUrl { get; set; }

        // Hayvan sahibi (üye)
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }

        public List<AsiKaydi> AsiKayitlari { get; set; } = new();
        public List<Randevu> Randevular { get; set; } = new();
    }
}