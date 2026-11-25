using System.Globalization;

namespace VeterinerKlinigi.Models
{
    public static class HizmetHelper
    {
        private static readonly CultureInfo Tr = new CultureInfo("tr-TR");

        // Hizmet adına göre uygun ikon
        public static string Ikon(string ad)
        {
            var a = (ad ?? string.Empty).ToLower(Tr);

            if (a.Contains("aşı")) return "bi-eyedropper";
            if (a.Contains("kısır") || a.Contains("ameliyat") || a.Contains("cerrahi")) return "bi-bandaid";
            if (a.Contains("diş")) return "bi-emoji-smile";
            if (a.Contains("tıraş") || a.Contains("bakım") || a.Contains("banyo")) return "bi-scissors";
            if (a.Contains("röntgen") || a.Contains("ultrason") || a.Contains("tahlil")) return "bi-activity";
            if (a.Contains("muayene") || a.Contains("kontrol")) return "bi-heart-pulse";
            return "bi-clipboard2-pulse";
        }

        // 30 → "30 dk", 90 → "1 sa 30 dk", 120 → "2 saat"
        public static string SureMetni(int dakika)
        {
            if (dakika < 60) return $"{dakika} dk";
            return dakika % 60 == 0 ? $"{dakika / 60} saat" : $"{dakika / 60} sa {dakika % 60} dk";
        }
    }
}