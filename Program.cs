using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı (appsettings.json'daki DefaultConnection)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection bulunamadı.");

builder.Services.AddDbContext<VeterinerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity (kullanıcı ve rol sistemi)
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Hocanın istediği "sau" şifresi kabul edilsin diye şifre kuralları gevşetildi
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<VeterinerContext>()
    .AddDefaultTokenProviders();

// Giriş yapılmadan korumalı bir sayfa açılırsa nereye yönlendirilecek?
builder.Services.ConfigureApplicationCookie(options =>
{
    // Sitenin kullanıcı tarafı (üye ve veteriner girişi) - bu sayfayı ileride yapacağız
    options.LoginPath = "/Hesap/Login";
    options.AccessDeniedPath = "/Hesap/Login";

    // Admin paneline (/Admin/...) giriş yapılmadan ya da yetkisiz girilirse yönetici giriş sayfasına gönder
    Task Yonlendir(Microsoft.AspNetCore.Authentication.RedirectContext<Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions> context)
    {
        if (context.Request.Path.StartsWithSegments("/Admin"))
        {
            var donusAdresi = context.Request.Path + context.Request.QueryString;
            context.Response.Redirect("/Admin/AdminGiris?returnUrl=" + Uri.EscapeDataString(donusAdresi));
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }
        return Task.CompletedTask;
    }

    options.Events.OnRedirectToLogin = Yonlendir;
    options.Events.OnRedirectToAccessDenied = Yonlendir;
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Rolleri ve admin hesabını oluştur
using (var scope = app.Services.CreateScope())
{
    await SeedData.BaslatAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Türkçe kültür (ondalık virgül, Türkçe tarih ve ay adları)
var trKultur = new CultureInfo("tr-TR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(trKultur),
    SupportedCultures = new List<CultureInfo> { trKultur },
    SupportedUICultures = new List<CultureInfo> { trKultur }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Admin paneli (Areas/Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();