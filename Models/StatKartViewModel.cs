namespace VeterinerKlinigi.Models
{
    public class StatKartViewModel
    {
        public string Baslik { get; set; } = string.Empty;
        public string Deger { get; set; } = string.Empty;
        public string Ikon { get; set; } = "bi-bar-chart";
        public string Ton { get; set; } = "teal";   // teal, blue, green, slate, amber, red
    }
}