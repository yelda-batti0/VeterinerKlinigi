namespace VeterinerKlinigi.Models
{
    public static class GunHelper
    {
        // Haftayı Pazartesi'den başlatmak için
        public static readonly DayOfWeek[] HaftaSirasi =
        {
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
            DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
        };

        public static string TurkceAd(DayOfWeek gun)
        {
            return gun switch
            {
                DayOfWeek.Monday => "Pazartesi",
                DayOfWeek.Tuesday => "Salı",
                DayOfWeek.Wednesday => "Çarşamba",
                DayOfWeek.Thursday => "Perşembe",
                DayOfWeek.Friday => "Cuma",
                DayOfWeek.Saturday => "Cumartesi",
                DayOfWeek.Sunday => "Pazar",
                _ => gun.ToString()
            };
        }

        public static string KisaAd(DayOfWeek gun)
        {
            return gun switch
            {
                DayOfWeek.Monday => "Pzt",
                DayOfWeek.Tuesday => "Sal",
                DayOfWeek.Wednesday => "Çar",
                DayOfWeek.Thursday => "Per",
                DayOfWeek.Friday => "Cum",
                DayOfWeek.Saturday => "Cmt",
                DayOfWeek.Sunday => "Paz",
                _ => gun.ToString()
            };
        }
    }
}