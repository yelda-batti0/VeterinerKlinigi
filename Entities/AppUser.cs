using Microsoft.AspNetCore.Identity;

namespace VeterinerKlinigi.Entities
{
    public class AppUser : IdentityUser
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;

        public List<EvcilHayvan> EvcilHayvanlar { get; set; } = new();
    }
}