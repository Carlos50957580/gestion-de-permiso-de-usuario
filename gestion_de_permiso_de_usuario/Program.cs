
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentAssertions.Common;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<DatabaseService>();

//Cookies y 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opcion =>
    {
        opcion.LoginPath = "/Login/Iniciar";
        opcion.ExpireTimeSpan = TimeSpan.FromMinutes(3);
        opcion.AccessDeniedPath = "/Home/Index";
        opcion.LogoutPath = "/Login/Logout";
        opcion.AccessDeniedPath = "/Login/AccessDenied";
    });

builder.Services.AddAuthorization( opcion =>
    {
        opcion.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
        opcion.AddPolicy("Supervisor", policy => policy.RequireRole("Supervisor"));
        opcion.AddPolicy("UsuarioBasico", policy => policy.RequireRole("UsuarioBasico"));
        opcion.AddPolicy("Invitado", policy => policy.RequireRole("Invitado"));
    });

builder.Services.AddControllersWithViews();
  




var app = builder.Build();

// Configurar la canalización HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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


