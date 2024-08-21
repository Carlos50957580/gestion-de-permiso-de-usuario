using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Filters;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class PermisosController : Controller
    {
        private readonly PermisoDataAccess _permisoDataAccess;

        public PermisosController(IConfiguration configuration)
        {
            _permisoDataAccess = new PermisoDataAccess(configuration);
        }
        [Permiso("Ver Permisos,Crear Permisos,Editar Permisos,Ver Detalles de Permisos")]
        // GET: Permisos/Index
        public IActionResult Index()
        {
            var permisos = _permisoDataAccess.GetPermisos();

            //Obtener el nombre del usuario autenticado
            var userName = User.Identity.Name;

            // Pasar el nombre del usuario a la vista a través del ViewBag
            ViewBag.UserName = userName;
            return View(permisos);
        }

        // GET: Permisos/Create
        //[Permiso("Crear Permisos")]
        public IActionResult Create()
        {
            return View();
        }



        // POST: Permisos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Crear Permisos")]
        public IActionResult Create([Bind("NombrePermiso,Descripcion,Estado")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                permiso.CreadoPor = User.Identity.Name; // Asigna el usuario que crea el permiso
                permiso.FechaCambio = DateTime.Now; // Asigna la fecha de creación
                _permisoDataAccess.AddPermiso(permiso);
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }

        // GET: Permisos/Edit
        [Permiso("Editar Permisos")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permiso = _permisoDataAccess.GetPermisoById(id.Value);
            if (permiso == null)
            {
                return NotFound();
            }

            return View(permiso);
        }

        // POST: Permisos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Permiso("Editar Permisos")]
        public IActionResult Edit(int id, [Bind("PermisoID,NombrePermiso,Descripcion,Estado,CreadoPor,ActualizadoPor")] Permiso permiso)
        {
            if (id != permiso.PermisoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Asigna el nombre del usuario que está realizando la actualización
                    permiso.ActualizadoPor = User.Identity.Name;

                    _permisoDataAccess.UpdatePermiso(permiso);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el permiso: " + ex.Message);
                    return View(permiso);
                }
            }
            return View(permiso);
        }

        // GET: Permisos/Details
        [Permiso("Ver Detalles de Permisos")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permiso = _permisoDataAccess.GetPermisoById(id.Value);
            if (permiso == null)
            {
                return NotFound();
            }

            return View(permiso);
        }
    }
}
