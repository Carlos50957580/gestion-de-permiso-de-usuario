using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using System.Data.SqlClient;
using System.Data;

namespace gestion_de_permiso_de_usuario.Data
{
    public class UserData : DatabaseService
    {
        public UserData(IConfiguration configuration) : base(configuration)
        {

        }

        public List<User> GetUser()
        {
            List<User> Lista = new List<User>();

            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT UsuarioID,NombreUsuario,PersonaID,RolID,Contraseña,FechaCambio,Estado, CreadoPor, ActualizadoPor FROM Usuarios", connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new User
                            {
                                UsuarioID = Convert.ToInt32(reader["UsuarioID"]), 
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                PersonaID = Convert.ToInt32(reader["PersonaID"]),
                                RolID = Convert.ToInt32(reader["RolID"]),
                                Contraseña = reader["Contraseña"].ToString(),
                                FechaCambio = reader["FechaCambio"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                CreadoPor = reader["CreadoPor"].ToString(),
                                ActualizadoPor = reader["ActualizadoPor"].ToString()
                              
                            });
                        }

                    }
                }
            }
            catch
            {
                //Lista = new List<User>();

            }

            return Lista;
        }
    }
}
