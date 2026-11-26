namespace VeterinerKlinigi.Dtos.EvcilHayvanDtos
{
    public class DeleteEvcilHayvanDto
    {
        public int EvcilHayvanId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Tur { get; set; } = string.Empty;
        public string? FotografUrl { get; set; }
        public int AsiSayisi { get; set; }
        public int RandevuSayisi { get; set; }
    }
}