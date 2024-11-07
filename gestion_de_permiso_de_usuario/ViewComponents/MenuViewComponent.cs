using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace gestion_de_permiso_de_usuario.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly MenuItemDataAccess _menuItemDataAccess;

        public MenuViewComponent(IConfiguration configuration)
        {
            _menuItemDataAccess = new MenuItemDataAccess(configuration);
        }

        public IViewComponentResult Invoke()
        {
            // Obtener el userId del usuario autenticado
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Llamar a GetMenuItems pasando el userId
            var menuItems = _menuItemDataAccess.GetMenuItems(userId);
            return View(menuItems);
        }
    }
}
