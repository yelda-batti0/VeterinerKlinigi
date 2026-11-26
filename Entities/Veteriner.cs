namespace VeterinerKlinigi.Entities
{
    public class Veteriner
    {
        public int VeterinerId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;

        public int KlinikId { get; set; }
        public Klinik? Klinik { get; set; }

        // Veterinerin sisteme giriş yaptığı hesap
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public List<UzmanlikAlani> UzmanlikAlanlari { get; set; } = new();
        public List<Hizmet> Hizmetler { get; set; } = new();
        public List<Musaitlik> Musaitlikler { get; set; } = new();
        public List<Randevu> Randevular { get; set; } = new();
    }
}