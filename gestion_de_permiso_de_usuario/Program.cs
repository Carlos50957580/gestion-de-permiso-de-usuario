using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<DatabaseService>();

// Registrar DataAccess en el contenedor de dependencias
builder.Services.AddScoped<UsuarioDataAccess>();
builder.Services.AddScoped<PersonaDataAccess>();
builder.Services.AddScoped<RolDataAccess>();

// Configuración de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opcion =>
    {
        opcion.LoginPath = "/Login/Iniciar";
        opcion.ExpireTimeSpan = TimeSpan.FromMinutes(3);
        
        opcion.LogoutPath = "/Login/Logout";
        opcion.AccessDeniedPath = "/Login/AccessDenied";
    });

// Configuración de autorización basada en roles
builder.Services.AddAuthorization(opcion =>
{
    opcion.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    opcion.AddPolicy("Supervisor", policy => policy.RequireRole("Supervisor"));
    opcion.AddPolicy("Invitado", policy => policy.RequireRole("Invitado"));
});

// Configurar la canalización HTTP.
var app = builder.Build();

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
