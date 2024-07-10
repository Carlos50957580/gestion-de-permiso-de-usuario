using System.ComponentModel.DataAnnotations;

namespace gestion_de_permiso_de_usuario.Models
{
    public class User
    {
        public int UsuarioID {  get; set; } 
        [Required]
        [StringLength(25)]
        public string NombreUsuario {  get; set; }
        public int PersonaID { get; set; }
        public int RolID { get; set; }    
        [Required]
        [StringLength(50)]
        public string Contraseña { get; set; }
        public string FechaCambio { get; set; }
        public bool Estado { get; set; }
        [Required]
        [StringLength(25)]
        public string CreadoPor { get; set; }
        [Required]
        [StringLength(25)]
        public string ActualizadoPor { get; set; }


        //Recuerda actualizar pendejo
    }
}
