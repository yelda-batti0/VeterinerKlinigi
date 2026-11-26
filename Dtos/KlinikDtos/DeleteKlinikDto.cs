namespace VeterinerKlinigi.Dtos.KlinikDtos
{
    public class DeleteKlinikDto
    {
        public int KlinikId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public int HizmetSayisi { get; set; }
        public int VeterinerSayisi { get; set; }
    }
}