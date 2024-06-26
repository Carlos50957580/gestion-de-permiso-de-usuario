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
            List<User> entities = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT nombe as Nombre FROM Userd", connection);
                command.CommandType = CommandType.Text;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(new User
                        {
                            Nombre = reader.GetString(0)
                        });
                    }

                }
            }

            return entities;
        }
    }
}
