using System.Globalization;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Models
{
    public static class VeterinerHelper
    {
        private static readonly CultureInfo Tr = new CultureInfo("tr-TR");

        public static string BasHarfler(VeterinerHekim v)
        {
            var a = string.IsNullOrEmpty(v.Ad) ? "" : v.Ad.Substring(0, 1);
            var s = string.IsNullOrEmpty(v.Soyad) ? "" : v.Soyad.Substring(0, 1);
            return (a + s).ToUpper(Tr);
        }

        // Bugünün müsaitlik aralıkları
        public static List<HekimMusaitlik> BugunkuAraliklar(VeterinerHekim v)
        {
            return v.Musaitlikler
                .Where(m => m.Gun == DateTime.Today.DayOfWeek)
                .OrderBy(m => m.BaslangicSaati)
                .ToList();
        }

        // Haftalık toplam çalışma süresi
        public static TimeSpan HaftalikSure(VeterinerHekim v)
        {
            return TimeSpan.FromMinutes(v.Musaitlikler.Sum(m => (m.BitisSaati - m.BaslangicSaati).TotalMinutes));
        }

        public static string SureMetni(TimeSpan sure)
        {
            var saat = (int)sure.TotalHours;
            return sure.Minutes == 0 ? $"{saat} saat" : $"{saat} sa {sure.Minutes} dk";
        }
    }
}