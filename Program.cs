using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using mvc_api.Database;

var builder = WebApplication.CreateBuilder(args);

//Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o => o.UseMySQL(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/LoginUsuario/"; 
        options.Cookie.HttpOnly = true;  // Bloquear acesso pelo JS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Apenas enviar via HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict;  // Limitar a origem do cookie para o mesmo site
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Tempo de expiração
        options.SlidingExpiration = true; // Tempo de expiração renova a cada acesso
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();