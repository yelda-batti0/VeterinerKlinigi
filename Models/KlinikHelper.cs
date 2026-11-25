using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public static class KlinikHelper
    {
        // Klinik şu an çalışma saatleri içinde mi?
        public static bool SuAnAcikMi(Klinik klinik)
        {
            var simdi = DateTime.Now.TimeOfDay;
            return simdi >= klinik.AcilisSaati && simdi < klinik.KapanisSaati;
        }

        public static string GunlukCalismaSuresi(Klinik klinik)
        {
            var sure = klinik.KapanisSaati - klinik.AcilisSaati;
            return sure.Minutes == 0
                ? $"{(int)sure.TotalHours} saat"
                : $"{(int)sure.TotalHours} sa {sure.Minutes} dk";
        }
    }
}