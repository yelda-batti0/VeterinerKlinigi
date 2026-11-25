using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public class UzmanlikIndexViewModel
    {
        public List<UzmanlikAlani> Uzmanliklar { get; set; } = new();
        public int ToplamUzmanlik { get; set; }
        public int AtanmisUzmanlik { get; set; }
        public int BostaUzmanlik { get; set; }
        public string EnYaygin { get; set; } = "-";
    }
}