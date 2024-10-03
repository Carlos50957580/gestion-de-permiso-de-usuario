using gestion_de_permiso_de_usuario.Models;
using System.Data;
using System.Data.SqlClient;

namespace gestion_de_permiso_de_usuario.Data
{
    public class RolDataAccess
    {
        private readonly string _connectionString;

        public RolDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddRol(Rol rol, string usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddRol", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", rol.Estado);
                    cmd.Parameters.AddWithValue("@CreadoPor", usuario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el rol: " + ex.Message);
            }
        }

        public List<Rol> GetRoles()
        {
            List<Rol> roles = new List<Rol>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetRoles", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            RolID = Convert.ToInt32(reader["RolID"]),
                            NombreRol = reader["NombreRol"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            ActualizadoPor = reader["ActualizadoPor"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles: " + ex.Message);
            }

            return roles;
        }

        public Rol GetRolById(int rolID)
        {
            Rol rol = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetRolById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolID", rolID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        rol = new Rol
                        {
                            RolID = Convert.ToInt32(reader["RolID"]),
                            NombreRol = reader["NombreRol"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            ActualizadoPor = reader["ActualizadoPor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rol: " + ex.Message);
            }

            return rol;
        }

        public void UpdateRol(Rol rol, string usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateRol", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolID", rol.RolID);
                    cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", rol.Estado);
                    cmd.Parameters.AddWithValue("@ActualizadoPor", usuario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el rol: " + ex.Message);
            }
        }

        public void AsignarPermisosARol(int rolID, int[] permisosSeleccionados)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmdDelete = new SqlCommand("spEliminarPermisosDeRol", conn);
                    cmdDelete.CommandType = CommandType.StoredProcedure;
                    cmdDelete.Parameters.AddWithValue("@RolID", rolID);
                    conn.Open();
                    cmdDelete.ExecuteNonQuery();

                    foreach (var permisoID in permisosSeleccionados)
                    {
                        SqlCommand cmdInsert = new SqlCommand("spAsignarPermisosARol", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;
                        cmdInsert.Parameters.AddWithValue("@RolID", rolID);
                        cmdInsert.Parameters.AddWithValue("@PermisoID", permisoID);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar permisos al rol: " + ex.Message);
            }
        }

        public List<Permiso> GetPermisosAsignados(int rolID)
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetPermisosAsignados", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolID", rolID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        permisos.Add(new Permiso
                        {
                            PermisoID = Convert.ToInt32(reader["PermisoID"]),
                            NombrePermiso = reader["NombrePermiso"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los permisos asignados: " + ex.Message);
            }

            return permisos;
        }

        public List<Rol> GetAllRoles()
        {
            var roles = new List<Rol>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT RolID, NombreRol FROM Roles";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rol = new Rol
                            {
                                RolID = Convert.ToInt32(reader["RolID"]),
                                NombreRol = reader["NombreRol"].ToString()
                            };
                            roles.Add(rol);
                        }
                    }
                }
            }

            return roles;
        }


        public int ObtenerTotalRoles()
        {
            int totalRoles = 0;
            string query = "SELECT COUNT(*) FROM Roles";  

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                totalRoles = (int)cmd.ExecuteScalar();
            }
            return totalRoles;
        }



        public int ObtenerRolesActivos()
        {
            int rolesActivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Roles WHERE Estado = 1";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                rolesActivos = (int)command.ExecuteScalar();
            }

            return rolesActivos;
        }

        public int ObtenerRolesInactivos()
        {
            int rolesInactivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Roles WHERE Estado = 0";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                rolesInactivos = (int)command.ExecuteScalar();
            }

            return rolesInactivos;
        }
    }
}

