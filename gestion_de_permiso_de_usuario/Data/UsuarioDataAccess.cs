using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace gestion_de_permiso_de_usuario.Data
{
    public class UsuarioDataAccess
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;



        public UsuarioDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;

        }

        // Obtiene una lista de usuarios con detalles adicionales
        public List<UsuarioConDetalles> GetUsuariosConDetalles()
        {
            var usuariosConDetalles = new List<UsuarioConDetalles>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "GetUsuariosConDetalles";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var usuarioConDetalles = new UsuarioConDetalles
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    NombreRol = reader["NombreRol"].ToString(),
                                    Estado = Convert.ToInt32(reader["Estado"])
                                };
                                usuariosConDetalles.Add(usuarioConDetalles);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios con detalles.", ex);
            }

            return usuariosConDetalles;
        }

        // Obtiene un usuario por su ID
        public Usuario GetUsuarioByID(int id)
        {
            Usuario usuario = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "GetUsuarioByID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioID", id);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new Usuario
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    PersonaID = Convert.ToInt32(reader["PersonaID"]),
                                    RolID = Convert.ToInt32(reader["RolID"]),
                                    FechaCambio = Convert.ToDateTime(reader["FechaCambio"]),
                                    Estado = Convert.ToInt32(reader["Estado"]),
                                    CreadoPor = reader["CreadoPor"].ToString(),
                                    ActualizadoPor = reader["ActualizadoPor"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por ID.", ex);
            }

            return usuario;
        }

        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Actualiza los datos de un usuario
        public void UpdateUsuario(Usuario usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UpdateUsuario";
                    using (var command = new SqlCommand(query, connection))
                    {
                        string encryptedPassword = EncryptPassword(usuario.Contraseña);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@PersonaID", usuario.PersonaID);
                        command.Parameters.AddWithValue("@RolID", usuario.RolID);
                        command.Parameters.AddWithValue("@Contraseña", encryptedPassword);
                        command.Parameters.AddWithValue("@Estado", usuario.Estado);
                        command.Parameters.AddWithValue("@CreadoPor", usuario.CreadoPor); // Opcional
                        command.Parameters.AddWithValue("@ActualizadoPor", usuario.ActualizadoPor); // Nuevo parámetro

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario.", ex);
            }
        }

        // Obtiene los detalles de un usuario por su ID
        public UsuarioConDetalles GetUsuarioConDetallesByID(int id)
        {
            UsuarioConDetalles usuarioConDetalles = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "GetUsuarioConDetallesByID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioID", id);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuarioConDetalles = new UsuarioConDetalles
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    NombreRol = reader["NombreRol"].ToString(),
                                    Contraseña = reader["Contraseña"].ToString(),
                                    Estado = Convert.ToInt32(reader["Estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles del usuario por ID.", ex);
            }

            return usuarioConDetalles;
        }

        // Inserta un nuevo usuario
        public void InsertUsuario(Usuario usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "InsertUsuario";
                    using (var command = new SqlCommand(query, connection))
                    {
                        string encryptedPassword = EncryptPassword(usuario.Contraseña);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@PersonaID", usuario.PersonaID);
                        command.Parameters.AddWithValue("@RolID", usuario.RolID);
                        command.Parameters.AddWithValue("@Contraseña", encryptedPassword);
                        command.Parameters.AddWithValue("@Estado", usuario.Estado);
                        command.Parameters.AddWithValue("@CreadoPor", usuario.CreadoPor);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario.", ex);
            }
        }

        // Obtiene el ID del rol "Base"
        public int GetRolBaseId()
        {
            int rolBaseId = 0;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "SELECT RolID FROM Roles WHERE NombreRol = 'Base'";
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        rolBaseId = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID del rol Base.", ex);
            }

            return rolBaseId;

        }
        public List<string> GetPermisosDeUsuario(string nombreUsuario)
        {
            // Consulta que obtiene los permisos del usuario basado en su nombre de usuario
            string query = @"
                SELECT P.NombrePermiso
                FROM Permisos p
                JOIN RolPermisos rp ON p.PermisoID = rp.PermisoID
                JOIN Roles r ON rp.RolID = r.RolID
                JOIN Usuarios u ON r.RolID = u.RolID
                WHERE u.NombreUsuario = @NombreUsuario";

            var permisos = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    permisos.Add(reader["NombrePermiso"].ToString());
                }
                reader.Close();
            }

            return permisos;
        }



        public int ObtenerTotalUsuarios()
        {
            int totalUsuarios = 0;
            string query = "SELECT COUNT(*) FROM Usuarios";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                totalUsuarios = (int)cmd.ExecuteScalar();
            }
            return totalUsuarios;
        }


   


        public int ObtenerUsuariosActivos()
        {
            int usuariosActivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Estado = 1";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                usuariosActivos = (int)command.ExecuteScalar();
            }

            return usuariosActivos;
        }

        public int ObtenerUsuariosInactivos()
        {
            int usuariosInactivos = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Estado = 0";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                usuariosInactivos = (int)command.ExecuteScalar();
            }

            return usuariosInactivos;
        }

        public List<UsuarioPorRol> ObtenerCantidadUsuariosPorRol()
        {
            List<UsuarioPorRol> usuariosPorRol = new List<UsuarioPorRol>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT R.NombreRol AS RolNombre, COUNT(U.UsuarioID) AS CantidadUsuarios
                FROM Roles R
                LEFT JOIN Usuarios U ON R.RolID = U.RolID
                GROUP BY R.NombreRol";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuariosPorRol.Add(new UsuarioPorRol
                        {
                            RolNombre = reader["RolNombre"].ToString(),
                            CantidadUsuarios = Convert.ToInt32(reader["CantidadUsuarios"])
                        });
                    }
                }
            }

            return usuariosPorRol;
        }
    }
}


