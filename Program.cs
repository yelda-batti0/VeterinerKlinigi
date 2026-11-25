using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinerKlinigi.Context;
using VeterinerKlinigi.Entities;
using VeterinerKlinigi.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})


    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // İstenen sayfa [Authorize(Roles = "Admin")] ile korunuyor mu?
    bool AdminSayfasiMi(HttpContext http) =>
        http.GetEndpoint()?.Metadata
            .GetOrderedMetadata<AuthorizeAttribute>()
            .Any(a => a.Roles != null && a.Roles.Split(',').Contains(Roller.Admin)) == true;

    void AdminGiriseYonlendir(HttpContext http)
    {
        var donusAdresi = http.Request.Path + http.Request.QueryString;
        http.Response.Redirect("/AdminGiris?returnUrl=" + Uri.EscapeDataString(donusAdresi));
    }

    // Giriş yapılmamışsa
    options.Events.OnRedirectToLogin = context =>
    {
        if (AdminSayfasiMi(context.HttpContext))
            AdminGiriseYonlendir(context.HttpContext);
        else
            context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };

    // Giriş yapılmış ama yetki yoksa (örn. üye hesabıyla admin sayfası açılırsa)
    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (AdminSayfasiMi(context.HttpContext))
            AdminGiriseYonlendir(context.HttpContext);
        else
            context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IKlinikService, KlinikService>();
builder.Services.AddScoped<IUzmanlikAlaniService, UzmanlikAlaniService>();
builder.Services.AddScoped<IHizmetService, HizmetService>();
builder.Services.AddScoped<IVeterinerService, VeterinerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedData.BaslatAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
var trKultur = new CultureInfo("tr-TR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(trKultur),
    SupportedCultures = new List<CultureInfo> { trKultur },
    SupportedUICultures = new List<CultureInfo> { trKultur }
});
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
