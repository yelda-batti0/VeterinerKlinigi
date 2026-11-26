namespace VeterinerKlinigi.Dtos.HizmetDtos
{
    public class DeleteHizmetDto
    {
        public int HizmetId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string KlinikAd { get; set; } = string.Empty;
        public int RandevuSayisi { get; set; }
    }
}