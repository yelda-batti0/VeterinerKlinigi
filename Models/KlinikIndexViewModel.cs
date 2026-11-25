using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public class KlinikIndexViewModel
    {
        public List<Klinik> Klinikler { get; set; } = new();
        public int ToplamKlinik { get; set; }
        public int AcikKlinik { get; set; }
        public int KapaliKlinik { get; set; }
        public int ToplamVeteriner { get; set; }
    }
}