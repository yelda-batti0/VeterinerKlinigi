namespace VeterinerKlinigi.Entities
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }

        // Randevu alındığı andaki süre ve ücret (hizmetin fiyatı sonradan değişse de kayıt korunur)
        public int SureDakika { get; set; }
        public decimal Ucret { get; set; }

        public RandevuDurum Durum { get; set; } = RandevuDurum.Bekliyor;
        public string? MuayeneNotu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        public int EvcilHayvanId { get; set; }
        public EvcilHayvan? EvcilHayvan { get; set; }

        public int HizmetId { get; set; }
        public Hizmet? Hizmet { get; set; }

        public int VeterinerId { get; set; }
        public Veteriner? Veteriner { get; set; }
    }
}