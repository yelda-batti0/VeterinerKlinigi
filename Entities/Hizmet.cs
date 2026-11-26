namespace VeterinerKlinigi.Entities
{
    public class Hizmet
    {
        public int HizmetId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public int SureDakika { get; set; }
        public decimal Ucret { get; set; }

        public int KlinikId { get; set; }
        public Klinik? Klinik { get; set; }

        public List<Veteriner> Veterinerler { get; set; } = new();
        public List<Randevu> Randevular { get; set; } = new();
    }
}