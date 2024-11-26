using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace gestion_de_permiso_de_usuario.Data
{
    public class PermisoDataAccess
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public PermisoDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
        }

        // Método para agregar un permiso
        public void AddPermiso(Permiso permiso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGestionarPermiso", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "A"); // Acción para agregar
                    cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);
                    cmd.Parameters.AddWithValue("@Descripcion", permiso.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", permiso.Estado);
                    cmd.Parameters.AddWithValue("@CreadoPor", permiso.CreadoPor);
                    cmd.Parameters.AddWithValue("@FechaCambio", permiso.FechaCambio);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el permiso: " + ex.Message);
            }
        }

        // Método para obtener todos los permisos
        public List<Permiso> GetPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGestionarPermiso", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "L"); // Acción para listar todos los permisos
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        permisos.Add(new Permiso
                        {
                            PermisoID = Convert.ToInt32(reader["PermisoID"]),
                            NombrePermiso = reader["NombrePermiso"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            FechaCambio = Convert.ToDateTime(reader["FechaCambio"]),
                            ActualizadoPor = reader["ActualizadoPor"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los permisos: " + ex.Message);
            }

            return permisos;
        }

        // Método para obtener un permiso por su ID
        public Permiso GetPermisoById(int permisoID)
        {
            Permiso permiso = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGestionarPermiso", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "S"); // Acción para obtener permiso por ID
                    cmd.Parameters.AddWithValue("@PermisoID", permisoID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        permiso = new Permiso
                        {
                            PermisoID = Convert.ToInt32(reader["PermisoID"]),
                            NombrePermiso = reader["NombrePermiso"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            FechaCambio = Convert.ToDateTime(reader["FechaCambio"]),
                            ActualizadoPor = reader["ActualizadoPor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el permiso: " + ex.Message);
            }

            return permiso;
        }

        // Método para actualizar un permiso
        public void UpdatePermiso(Permiso permiso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGestionarPermiso", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "U"); // Acción para actualizar
                    cmd.Parameters.AddWithValue("@PermisoID", permiso.PermisoID);
                    cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);
                    cmd.Parameters.AddWithValue("@Descripcion", permiso.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", permiso.Estado);
                    cmd.Parameters.AddWithValue("@ActualizadoPor", permiso.ActualizadoPor);
                    cmd.Parameters.AddWithValue("@FechaCambio", permiso.FechaCambio);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el permiso: " + ex.Message);
            }
        }

        

        public List<string> ObtenerPermisosPorUsuario(int userId)
        {
            var permisos = new List<string>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                // Consulta utilizando joins
                string query = @"
            SELECT p.NombrePermiso 
            FROM Usuarios u
            INNER JOIN Roles r ON u.RolID = r.RolID
            INNER JOIN RolPermisos rp ON r.RolID = rp.RolID
            INNER JOIN Permisos p ON rp.PermisoID = p.PermisoID
            WHERE u.UsuarioID = @UserID AND p.Estado = 1"; // Filtrar solo permisos activos

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permisos.Add(reader.GetString(0)); // Asumiendo que la columna es de tipo string
                        }
                    }
                }
            }

            return permisos;
        }



        public int ObtenerTotalPermisos()
        {
            int totalPermisos = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Permisos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    totalPermisos = (int)cmd.ExecuteScalar();
                }
            }

            return totalPermisos;
        }


        public int ObtenerPermisosActivos()
        {
            int permisosActivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Permisos WHERE Estado = 1";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                permisosActivos = (int)command.ExecuteScalar();
            }

            return permisosActivos;
        }

        public int ObtenerPermisosInactivos()
        {
            int permisosInactivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Permisos WHERE Estado = 0";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                permisosInactivos = (int)command.ExecuteScalar();
            }

            return permisosInactivos;
       
        
        
        
        }


    }



}



