using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

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

        // GET: Usuarios/Index
        public IActionResult Index()
        {
            var usuarios = _usuarioDataAccess.GetUsuariosConDetalles();
            return View(usuarios);
        }

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

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NombreUsuario,PersonaID,RolID,Contraseña,Estado,CreadoPor")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioDataAccess.InsertUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Personas = _personaDataAccess.GetAllPersonas();
            ViewBag.Roles = _rolDataAccess.GetAllRoles();
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
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
        public IActionResult Edit(int id, [Bind("UsuarioID,NombreUsuario,PersonaID,RolID,Contraseña,Estado,CreadoPor")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
