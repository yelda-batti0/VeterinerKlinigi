namespace VeterinerKlinigi.Models
{
    public class BosDurumViewModel
    {
        public string Ikon { get; set; } = "bi-inbox";
        public string Baslik { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public string? ButonMetni { get; set; }
        public string? ButonUrl { get; set; }
    }
}