using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Filters;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioDataAccess _usuarioDataAccess;
        private readonly PersonaDataAccess _personaDataAccess;
        private readonly RolDataAccess _rolDataAccess;

        public UsuariosController(IConfiguration configuration)
        {
            _usuarioDataAccess = new UsuarioDataAccess(configuration);
            _personaDataAccess = new PersonaDataAccess(configuration);
            _rolDataAccess = new RolDataAccess(configuration);
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
        public IActionResult Create([Bind("NombreUsuario,PersonaID,Contraseña,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.CreadoPor = User.Identity.Name; // Asignar el usuario actual al campo CreadoPor

                // Obtener el ID del rol Base
                int rolBaseId = _usuarioDataAccess.GetRolBaseId();
                usuario.RolID = rolBaseId;

                _usuarioDataAccess.InsertUsuario(usuario);
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
