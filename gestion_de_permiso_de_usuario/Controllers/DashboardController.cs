using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Filters;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestion_de_permiso_de_usuario.Controllers
{
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly UsuarioDataAccess _usuarioDataAccess;
        private readonly PersonaDataAccess _personaDataAccess;
        private readonly RolDataAccess _rolDataAccess;
        private readonly PermisoDataAccess _permisoDataAccess;

        public DashboardController(IConfiguration configuration)
        {
            _usuarioDataAccess = new UsuarioDataAccess(configuration);
            _personaDataAccess = new PersonaDataAccess(configuration);
            _rolDataAccess = new RolDataAccess(configuration);
            _permisoDataAccess = new PermisoDataAccess(configuration);
        }

        [HttpGet("Index")]
        [Permiso("Ver Dashboard")]
        public IActionResult Index()
        {
            var usuarios = _usuarioDataAccess.GetUsuariosConDetalles();
            var personas = _personaDataAccess.GetPersonas();
            var roles = _rolDataAccess.GetRoles();
            var permisos = _permisoDataAccess.GetPermisos();

            var model = new Tuple<IEnumerable<UsuarioConDetalles>, IEnumerable<Persona>>(usuarios, personas);

            ViewBag.Roles = roles;
            ViewBag.Permisos = permisos;

            ViewBag.TotalUsuarios = usuarios.Count();
            ViewBag.TotalPersonas = personas.Count();
            ViewBag.TotalRoles = roles.Count();
            ViewBag.TotalPermisos = permisos.Count();

            var usuariosPorRol = roles.Select(rol => new
            {
                RolNombre = rol.NombreRol,
                CantidadUsuarios = usuarios.Count(u => u.NombreRol == rol.NombreRol)
            }).ToList();

            ViewBag.UsuariosPorRolLabels = string.Join(",", usuariosPorRol.Select(ur => $"'{ur.RolNombre}'"));
            ViewBag.UsuariosPorRolData = string.Join(",", usuariosPorRol.Select(ur => ur.CantidadUsuarios));

            var activosUsuarios = usuarios.Count(u => u.Estado == (int)EstadoUsuario.Activo);
            var inactivosUsuarios = usuarios.Count(u => u.Estado == (int)EstadoUsuario.Inactivo);

            ViewBag.ActivosUsuarios = activosUsuarios;
            ViewBag.InactivosUsuarios = inactivosUsuarios;

            var activosRoles = roles.Count(r => r.Estado == (int)EstadoRol.Activo);
            var inactivosRoles = roles.Count(r => r.Estado == (int)EstadoRol.Inactivo);

            ViewBag.ActivosRoles = activosRoles;
            ViewBag.InactivosRoles = inactivosRoles;

            var activosPermisos = permisos.Count(p => p.Estado == (int)EstadoPermiso.Activo);
            var inactivosPermisos = permisos.Count(p => p.Estado == (int)EstadoPermiso.Inactivo);

            ViewBag.ActivosPermisos = activosPermisos;
            ViewBag.InactivosPermisos = inactivosPermisos;

            return View(model);
        }
    }
}
