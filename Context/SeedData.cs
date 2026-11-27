using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Entities;

namespace VeterinerKlinigi.Context
{
    public static class SeedData
    {
        public static async Task BaslatAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<VeterinerContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Bekleyen migration varsa uygula
            await context.Database.MigrateAsync();

            // Rolleri oluştur
            foreach (var rol in new[] { Roller.Admin, Roller.Veteriner, Roller.Uye })
            {
                if (!await roleManager.RoleExistsAsync(rol))
                    await roleManager.CreateAsync(new IdentityRole(rol));
            }

            // Admin kullanıcısını oluştur
           
            var adminEmail = "b241200023@sakarya.edu.tr";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Ad = "Sistem",
                    Soyad = "Yöneticisi"
                };
                await userManager.CreateAsync(admin, "sau");
            }

            if (!await userManager.IsInRoleAsync(admin, Roller.Admin))
                await userManager.AddToRoleAsync(admin, Roller.Admin);
        }
    }
}