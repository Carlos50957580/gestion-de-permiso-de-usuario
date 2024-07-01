using gestion_de_permiso_de_usuario.Clases;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<DatabaseService>();
builder.Services.AddTransient<UserData>();
builder.Services.AddTransient<CN_Usuario>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


