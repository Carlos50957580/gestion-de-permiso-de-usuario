using System;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        public string NombrePermiso { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; } = 1; // Estado activo por defecto
        public string CreadoPor { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string ActualizadoPor { get; set; }
    }
}
