using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            List<User> pLista = new List<User>();

            pLista = new UserController().GetUser();

            return Json(pLista);

        }
    }
}
