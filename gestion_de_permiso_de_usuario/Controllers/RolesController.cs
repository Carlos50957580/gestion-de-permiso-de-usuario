using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using gestion_de_permiso_de_usuario.Filters;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class RolesController : Controller
    {
        private readonly RolDataAccess _rolDataAccess;
        private readonly PermisoDataAccess _permisoDataAccess;

        public RolesController(IConfiguration configuration)
        {
            _rolDataAccess = new RolDataAccess(configuration);
            _permisoDataAccess = new PermisoDataAccess(configuration);
        }

        // GET: Roles/Create
        [Permiso("Crear Roles")]
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Crear Roles")]
        public IActionResult Create([Bind("NombreRol,Descripcion,Estado")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                string usuario = User.Identity.Name;
                _rolDataAccess.AddRol(rol, usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        [Permiso("Ver Roles")]
        // GET: Roles/Index
        public IActionResult Index()
        {
            //Obtener el nombre del usuario autenticado
            var userName = User.Identity.Name;

            // Pasar el nombre del usuario a la vista a través del ViewBag
            ViewBag.UserName = userName;
            var roles = _rolDataAccess.GetRoles();

            return View(roles);
        }

        // GET: Roles/Edit/5
        [Permiso("Editar Roles")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = _rolDataAccess.GetRolById(id.Value);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Editar Roles")]
        public IActionResult Edit(int id, [Bind("RolID,NombreRol,Descripcion,Estado")] Rol rol)
        {
            if (id != rol.RolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string usuario = User.Identity.Name;
                    _rolDataAccess.UpdateRol(rol, usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el rol: " + ex.Message);
                    return View(rol);
                }
            }
            return View(rol);
        }

        [Permiso("Ver Detalles de roles")]
        // GET: Roles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = _rolDataAccess.GetRolById(id.Value);
            if (rol == null)
            {
                return NotFound();
            }

            var permisosAsignados = _rolDataAccess.GetPermisosAsignados(rol.RolID);
            ViewBag.PermisosAsignados = permisosAsignados;

            return View(rol);
        }

        // GET: Roles/Asignar/5
        [Permiso("Asignar Permisos")] 
        public IActionResult Asignar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = _rolDataAccess.GetRolById(id.Value);
            if (rol == null)
            {
                return NotFound();
            }

            var permisos = _permisoDataAccess.GetPermisos();
            ViewBag.Permisos = permisos;
            ViewBag.RolID = rol.RolID;
            ViewBag.NombreRol = rol.NombreRol;

            return View();
        }

        // POST: Roles/Asignar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Asignar Permisos")]
        public IActionResult Asignar(int rolId, int[] permisosSeleccionados)
        {
            try
            {
                _rolDataAccess.AsignarPermisosARol(rolId, permisosSeleccionados);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al asignar permisos al rol: " + ex.Message);
                return View();
            }
        }
    }
}
