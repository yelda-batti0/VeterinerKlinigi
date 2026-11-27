using System.Globalization;
using Microsoft.AspNetCore.Authorization;
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

    // İstenen sayfa [Authorize(Roles = "Admin")] ile mi korunuyor?
    bool AdminSayfasiMi(HttpContext http) =>
        http.GetEndpoint()?.Metadata
            .GetOrderedMetadata<AuthorizeAttribute>()
            .Any(a => a.Roles != null && a.Roles.Split(',').Contains(Roller.Admin)) == true;

    // Admin sayfasıysa yönetici giriş sayfasına, değilse sitenin giriş sayfasına gönder
    void Yonlendir(HttpContext http, string varsayilanAdres)
    {
        if (AdminSayfasiMi(http))
        {
            var donusAdresi = http.Request.Path + http.Request.QueryString;
            http.Response.Redirect("/AdminGiris?returnUrl=" + Uri.EscapeDataString(donusAdresi));
        }
        else
        {
            http.Response.Redirect(varsayilanAdres);
        }
    }

    options.Events.OnRedirectToLogin = context =>
    {
        Yonlendir(context.HttpContext, context.RedirectUri);
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        Yonlendir(context.HttpContext, context.RedirectUri);
        return Task.CompletedTask;
    };
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
    app.UseExceptionHandler("/Default/Error");
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

// /admin yazınca panel açılsın
app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();