using gestion_de_permiso_de_usuario.Clases;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CN_Usuario _cnUsuario;
        private readonly CN_Persona _cnPersona; 

        public HomeController(ILogger<HomeController> logger, CN_Usuario cnUsuario, CN_Persona cnPersona)
        {
            _logger = logger;
            _cnUsuario = cnUsuario;
            _cnPersona = cnPersona;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult ListaUsuarios()
        {
            List<User> uLista = _cnUsuario.GetUser();
            return Json( new { data = uLista });
        }

        public JsonResult ListaPersonas()
        {
            List<People> pLista = _cnPersona.GetPeople();
            return Json(new { dataP = pLista });
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Personas()
        {
            return View();  
        }

    }
}
