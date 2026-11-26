namespace VeterinerKlinigi.Dtos.KlinikDtos
{
    public class ResultKlinikDto
    {
        public int KlinikId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string? Telefon { get; set; }
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }
        public int HizmetSayisi { get; set; }
        public int VeterinerSayisi { get; set; }
    }
}