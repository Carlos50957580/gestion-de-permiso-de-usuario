using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Persona
    {
        public int PersonaID { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(25)]
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string FechaNacimiento2 { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        [Required]
        public string Cedula { get; set; }  

    }
}
