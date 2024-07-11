using System;
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
        public IActionResult Create([Bind("NombrePermiso,Descripcion,CreadoPor")] Permiso permiso)
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

        // GET: Permisos/Edit
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
