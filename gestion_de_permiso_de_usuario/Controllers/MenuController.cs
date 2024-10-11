using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuItemDataAccess _menuItemDataAccess;

        public MenuController(IConfiguration configuration)
        {
            _menuItemDataAccess = new MenuItemDataAccess(configuration);
        }

        public IActionResult Index()
        {
            var menuItems = _menuItemDataAccess.GetMenuItems();
            return View(menuItems);
        }
    }
}
