using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MenuViewComponent : ViewComponent
{
    private readonly MenuItemDataAccess _menuItemDataAccess;

    public MenuViewComponent(MenuItemDataAccess menuItemDataAccess)
    {
        _menuItemDataAccess = menuItemDataAccess;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Obtener todos los ítems del menú desde la base de datos
        var menuItems = await Task.Run(() => _menuItemDataAccess.GetMenuItems());

        // Separar los ítems que no tienen ParentID (los ítems principales)
        var parentItems = menuItems.Where(x => x.ParentID == null).ToList();

        // Para cada ítem principal, encontrar sus submenús (hijos)
        foreach (var parent in parentItems)
        {
            parent.SubMenuItems = menuItems.Where(x => x.ParentID == parent.MenuItemID).ToList();
        }

        return View(parentItems); // Pasar los ítems del menú a la vista
    }
}
