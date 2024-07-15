using System;
using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class RolesController : Controller
    {
        private readonly RolDataAccess _rolDataAccess;

        public RolesController(IConfiguration configuration)
        {
            _rolDataAccess = new RolDataAccess(configuration);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombreRol,Descripcion")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                _rolDataAccess.AddRol(rol);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: Roles/Index
        public IActionResult Index()
        {
            var roles = _rolDataAccess.GetRoles();
            return View(roles);
        }

        // GET: Roles/Edit
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

        // POST: Roles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    _rolDataAccess.UpdateRol(rol);
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

        // GET: Roles/Details
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

            return View(rol);
        }
    }
}