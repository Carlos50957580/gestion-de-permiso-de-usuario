using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace gestion_de_permiso_de_usuario.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static List<Role> GetRoles(string connectionString)
        {
            var roles = new List<Role>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT Id, Nombre FROM Roles", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }

            return roles;
        }
    }
}
