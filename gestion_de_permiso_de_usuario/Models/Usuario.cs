namespace gestion_de_permiso_de_usuario.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public int PersonaID { get; set; }
        public int RolID { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public int Estado { get; set; } = 1;
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
    }

    public enum EstadoUsuario
    {
        Activo = 1,
        Inactivo = 0
    }
}
