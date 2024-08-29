using gestion_de_permiso_de_usuario.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;


namespace gestion_de_permiso_de_usuario.Data
    {
        public class MenuDataAccess
        {
            private readonly string connectionString;
            public MenuDataAccess(IConfiguration configuration)
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            public List<Menu> GetMenuItems()
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var query = "SELECT * FROM MenuItems";
                    return connection.Query<Menu>(query).AsList();
                }
            }
        }
    }

