using System.Globalization;

namespace VeterinerKlinigi.Models
{
    public static class UzmanlikHelper
    {
        private static readonly CultureInfo Tr = new CultureInfo("tr-TR");

        // Formda tek tıkla doldurmak için öneriler
        public static readonly string[] Oneriler =
        {
            "Küçük Hayvan", "Egzotik Hayvan", "Cerrahi", "Dermatoloji",
            "Dahiliye", "Kardiyoloji", "Ortopedi", "Oftalmoloji"
        };

        public static string Ikon(string ad)
        {
            var a = (ad ?? string.Empty).ToLower(Tr);

            if (a.Contains("küçük")) return "bi-house-heart";
            if (a.Contains("egzotik")) return "bi-feather";
            if (a.Contains("cerrahi")) return "bi-bandaid";
            if (a.Contains("dermatoloji") || a.Contains("deri")) return "bi-droplet-half";
            if (a.Contains("kardiyoloji") || a.Contains("kalp")) return "bi-heart-pulse";
            if (a.Contains("ortopedi")) return "bi-person-walking";
            if (a.Contains("oftalmoloji") || a.Contains("göz")) return "bi-eye";
            if (a.Contains("diş")) return "bi-emoji-smile";
            if (a.Contains("onkoloji")) return "bi-shield-plus";
            if (a.Contains("radyoloji") || a.Contains("görüntüleme")) return "bi-activity";
            if (a.Contains("dahiliye")) return "bi-clipboard2-pulse";
            return "bi-award";
        }
    }
}