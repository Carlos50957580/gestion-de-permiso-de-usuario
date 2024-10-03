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

// Registrar IHttpContextAccessor para acceso al contexto HTTP
builder.Services.AddHttpContextAccessor();

// Agregar soporte para la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Aumenta la seguridad
    options.Cookie.IsEssential = true; // Necesario para el funcionamiento de la sesión
});

// Configuración de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Iniciar";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Ajustar según sea necesario
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

// Configuración de autorización basada en roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Supervisor", policy => policy.RequireRole("Supervisor"));
    options.AddPolicy("Invitado", policy => policy.RequireRole("Invitado"));
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

// Agregar middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Activar el middleware de sesiones
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
