using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuDataAccess _menuDataAccess;

        public MenuController(IConfiguration configuration)
        {
            _menuDataAccess = new MenuDataAccess(configuration);
        }

        public IActionResult Index()
        {
            List<Menu> menuItems = _menuDataAccess.GetMenuItems();
            return View(menuItems);
        }
    }
}
