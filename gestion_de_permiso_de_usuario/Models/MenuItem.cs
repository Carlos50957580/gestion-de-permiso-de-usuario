public class MenuItem
{
    public int MenuItemID { get; set; }
    public string Descripcion { get; set; }
    public string Controlador { get; set; }
    public string Accion { get; set; }
    public string Icono { get; set; }
    public int? ParentID { get; set; }

    // Propiedad para submenús
    public List<MenuItem> SubMenuItems { get; set; }
}
