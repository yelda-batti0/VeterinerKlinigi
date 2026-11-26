namespace VeterinerKlinigi.Dtos.VeterinerDtos
{
    public class ResultVeterinerDto
    {
        public int VeterinerId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string AdSoyad => $"Vet. Hek. {Ad} {Soyad}";
        public string? Email { get; set; }
        public int KlinikId { get; set; }
        public string KlinikAd { get; set; } = string.Empty;
        public List<string> Uzmanliklar { get; set; } = new();
        public List<string> Hizmetler { get; set; } = new();
        public List<DayOfWeek> CalismaGunleri { get; set; } = new();
        public int HaftalikCalismaDakika { get; set; }
        public int RandevuSayisi { get; set; }
    }
}