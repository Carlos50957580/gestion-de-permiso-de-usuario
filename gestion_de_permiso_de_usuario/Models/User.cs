namespace gestion_de_permiso_de_usuario.Models
{
    public class User
    {
        public int UsuarioID {  get; set; }
        public string NombreUsuario {  get; set; }
        public int PersonaID { get; set; }
        public int RolID { get; set; }
        public string Contraseña { get; set; }
        public string FechaCambio { get; set; }
        public bool Estado { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }


        //Recuerda actualizar pendejo
    }


    /*,[Nombre]
      ,[Apellido]
      ,[FechaNacimiento]
      ,[Genero]
      ,[Telefono]
      ,[Correo]*/

    public class Personas
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string FechaNacimiento { get; set; }

        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
