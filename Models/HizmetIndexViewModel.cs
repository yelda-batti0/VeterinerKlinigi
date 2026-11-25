using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public class HizmetIndexViewModel
    {
        public List<Hizmet> Hizmetler { get; set; } = new();
        public List<Klinik> Klinikler { get; set; } = new();
        public int? SeciliKlinikId { get; set; }

        public int ToplamHizmet { get; set; }
        public decimal OrtalamaUcret { get; set; }
        public int OrtalamaSure { get; set; }
        public int VeterinersizHizmet { get; set; }
    }
}