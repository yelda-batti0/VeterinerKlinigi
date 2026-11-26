namespace VeterinerKlinigi.Dtos.EvcilHayvanDtos
{
    public class ResultEvcilHayvanDto
    {
        public int EvcilHayvanId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Tur { get; set; } = string.Empty;
        public string? Irk { get; set; }
        public DateTime DogumTarihi { get; set; }
        public decimal Kilo { get; set; }
        public string? FotografUrl { get; set; }
        public string SahipAdSoyad { get; set; } = string.Empty;
        public int AsiSayisi { get; set; }
        public DateTime? YaklasanAsiTarihi { get; set; }
    }
}