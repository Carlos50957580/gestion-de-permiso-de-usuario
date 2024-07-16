using System;
using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        [Required]
        [StringLength(25)]
        public string NombrePermiso { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
        [Required]
        public int Estado { get; set; } = 1; // Estado activo por defecto
        [Required]
        [StringLength(25)]
        public string CreadoPor { get; set; } = "Sistema";
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string ActualizadoPor { get; set; } = "Sistema";
    }

    public enum EstadoPermiso
    {
        Activo = 1,
        Inactivo = 0
    }
}
