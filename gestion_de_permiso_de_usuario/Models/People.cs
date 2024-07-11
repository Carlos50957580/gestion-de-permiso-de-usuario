using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class People
    {
        public int PersonaID { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required]
        [StringLength(50)]
        public string FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public int Telefono { get; set; }
        [Required]
        [StringLength(25)]
        public string Correo { get; set; }
        [Required]
        [StringLength(25)]
        public string FechaCambio { get; set; }
    }
}
