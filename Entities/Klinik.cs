namespace VeterinerKlinigi.Entities
{
    public class Klinik
    {
        public int KlinikId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string? Telefon { get; set; }
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }

        public List<Hizmet> Hizmetler { get; set; } = new();
        public List<Veteriner> Veterinerler { get; set; } = new();
    }
}