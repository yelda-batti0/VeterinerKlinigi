namespace VeterinerKlinigi.Dtos.HizmetDtos
{
    public class ResultHizmetDto
    {
        public int HizmetId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public int SureDakika { get; set; }
        public decimal Ucret { get; set; }
        public int KlinikId { get; set; }
        public string KlinikAd { get; set; } = string.Empty;
        public int VeterinerSayisi { get; set; }
        public int RandevuSayisi { get; set; }
    }
}