using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class PermisosController : Controller
    {
        private readonly PermisoDataAccess _permisoDataAccess;

        public PermisosController(IConfiguration configuration)
        {
            _permisoDataAccess = new PermisoDataAccess(configuration);
        }

        // GET: Permisos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Permisos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombrePermiso,Descripcion,CreadoPor,ActualizadoPor")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _permisoDataAccess.AddPermiso(permiso);
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }

        // GET: Permisos/Index
        public IActionResult Index()
        {
            var permisos = _permisoDataAccess.GetPermisos();
            return View(permisos);
        }
    }
}
