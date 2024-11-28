using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Filters;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioDataAccess _usuarioDataAccess;
        private readonly PersonaDataAccess _personaDataAccess;
        private readonly RolDataAccess _rolDataAccess;
        private readonly EmailService _emailService; // Servicio de correo

        public UsuariosController(IConfiguration configuration, EmailService emailService)
        {
            _usuarioDataAccess = new UsuarioDataAccess(configuration);
            _personaDataAccess = new PersonaDataAccess(configuration);
            _rolDataAccess = new RolDataAccess(configuration);
            _emailService = emailService; // Inyectar el servicio de correo
        }

        [Permiso("Ver Usuarios,Ver Detalles de Usuarios,Crear Usuarios,Editar Usuarios")]
        // GET: Usuarios/Index
        public IActionResult Index()
        {
            var usuarios = _usuarioDataAccess.GetUsuariosConDetalles();

            // Obtener el nombre del usuario autenticado
            var userName = User.Identity.Name;

            // Pasar el nombre del usuario a la vista a través del ViewBag
            ViewBag.UserName = userName;
            return View(usuarios);
        }

        [Permiso("Ver Detalles de Usuarios")]
        // GET: Usuarios/Details/5
        public IActionResult Details(int id)
        {
            var usuario = _usuarioDataAccess.GetUsuarioConDetallesByID(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [Permiso("Crear Usuarios")]
        public IActionResult Create()
        {
            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreUsuario,PersonaID,Contraseña,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.CreadoPor = User.Identity.Name; // Asignar el usuario actual al campo CreadoPor

                // Obtener el ID del rol Base
                int rolBaseId = _usuarioDataAccess.GetRolBaseId();
                usuario.RolID = rolBaseId;

                // Insertar el usuario en la base de datos
                _usuarioDataAccess.InsertUsuario(usuario);

                // Obtener la persona asociada al usuario
                var persona = _personaDataAccess.GetPersonaById(usuario.PersonaID);

                // Crear el contenido del correo
                string subject = "Confirmación de Creación de Usuario";
                string body = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Confirmación de Creación de Usuario</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            color: #333;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }}
        h1 {{
            color: #4CAF50;
            text-align: center;
        }}
        .content {{
            font-size: 16px;
            line-height: 1.6;
        }}
        .content strong {{
            color: #333;
        }}
        .footer {{
            text-align: center;
            font-size: 14px;
            color: #888;
            margin-top: 20px;
        }}
        .button {{
            display: inline-block;
            background-color: #4CAF50;
            color: #fff;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
        .button:hover {{
            background-color: #45a049;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>¡Bienvenido a ReyCompany!</h1>
        <div class='content'>
            <p>Hola <strong>{persona.Nombre}</strong>,</p>
            <p>Tu cuenta ha sido creada exitosamente. A continuación, los detalles de tu acceso:</p>
            <p><strong>Usuario:</strong> {usuario.NombreUsuario}</p>
            <p><strong>Contraseña:</strong> {usuario.Contraseña}</p>
            <p>Por favor, asegúrate de cambiar tu contraseña después de iniciar sesión por primera vez.</p>
        </div>
        <div class='footer'>
            <p>Saludos,<br>El equipo de soporte de <strong>ReyCompany</strong></p>
        </div>
    </div>
</body>
</html>
";





                // Enviar correo de confirmación con usuario y contraseña
                await _emailService.SendEmailAsync(persona.Correo, subject, body);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Permiso("Editar Usuarios")]
        public IActionResult Edit(int id)
        {
            var usuario = _usuarioDataAccess.GetUsuarioByID(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Editar Usuarios")]
        public IActionResult Edit(int id, [Bind("UsuarioID,NombreUsuario,PersonaID,RolID,Contraseña,Estado")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _usuarioDataAccess.GetUsuarioByID(usuario.UsuarioID);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    usuario.CreadoPor = existingUser.CreadoPor; // Mantener el valor original para CreadoPor
                    usuario.ActualizadoPor = User.Identity.Name; // Asignar el usuario actual al campo ActualizadoPor

                    _usuarioDataAccess.UpdateUsuario(usuario);
                }
                catch
                {
                    if (_usuarioDataAccess.GetUsuarioByID(usuario.UsuarioID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View(usuario);
        }
    }
}
