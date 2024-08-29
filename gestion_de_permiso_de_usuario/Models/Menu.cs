using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Menu
    {
        public int MenuItemID { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public string Icono { get; set; }
        public int ParentID { get; set; }
       
    }
}
