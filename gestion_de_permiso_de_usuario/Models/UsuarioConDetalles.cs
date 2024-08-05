namespace gestion_de_permiso_de_usuario.Models
{
    public class UsuarioConDetalles
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreRol { get; set; }
        public int Estado { get; set; }
        public string Contraseña { get; set; }
    }
}
