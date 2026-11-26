namespace VeterinerKlinigi.Dtos.VeterinerDtos
{
    public class DeleteVeterinerDto
    {
        public int VeterinerId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string AdSoyad => $"Vet. Hek. {Ad} {Soyad}";
        public string KlinikAd { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int RandevuSayisi { get; set; }
    }
}