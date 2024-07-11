using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using System.Data.SqlClient;
using System.Data;
namespace gestion_de_permiso_de_usuario.Data
{
    public class PersonaData : DatabaseService
    {
        public PersonaData(IConfiguration configuration) : base(configuration)
        {

        }

        public List<People> GetPeople()
        {
            List<People> Lista1 = new List<People>();

            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT PersonaID,Nombre,Apellido,FechaNacimiento,Genero,Telefono,Correo, FechaCambio FROM Personas", connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista1.Add(new People
                            {
                                PersonaID = Convert.ToInt32(reader["PersonaID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                FechaNacimiento = reader["FechaNacimiento"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                Telefono = Convert.ToInt32(reader["Telefono"]),
                                Correo = reader["Correo"].ToString(),
                                FechaCambio = reader["FechaCambio"].ToString()

                            });
                        }

                    }
                }
            }
            catch
            {
                //Lista = new List<User>();

            }

            return Lista1;
        }
    }
}
