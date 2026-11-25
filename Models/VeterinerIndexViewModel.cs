using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public class VeterinerIndexViewModel
    {
        public List<VeterinerHekim> Veterinerler { get; set; } = new();
        public List<Klinik> Klinikler { get; set; } = new();
        public int? SeciliKlinikId { get; set; }

        public int ToplamVeteriner { get; set; }
        public int BugunCalisan { get; set; }
        public int MusaitlikGirilmemis { get; set; }
        public int KlinikSayisi { get; set; }
    }
}