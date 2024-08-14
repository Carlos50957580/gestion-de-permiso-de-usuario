using System;
using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Rol
    {
        public int RolID { get; set; }

        [Required]
        [StringLength(25)]
        public string NombreRol { get; set; }

        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required]
        public int Estado { get; set; } = (int)EstadoRol.Activo;

        public string CreadoPor { get; set; } = "Sistema";
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string ActualizadoPor { get; set; } = "Sistema";
    }

    public enum EstadoRol
    {
        Activo = 1,
        Inactivo = 0
    }
}