namespace gestion_de_permiso_de_usuario.Models
{
    public class Usuarios
    {
        public int User_id{ get; set; }
        public int Correo { get; set; }
        public int Clave { get; set; }

        public string ComfimarClave { get; set; }
    }
}
