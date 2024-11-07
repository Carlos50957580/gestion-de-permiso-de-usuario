using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

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
            // Obtiene el ID del usuario actual desde las claims
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Llama al método GetMenuItems con el ID del usuario
            var menuItems = _menuItemDataAccess.GetMenuItems(userId);
            return View(menuItems);
        }
    }
}
