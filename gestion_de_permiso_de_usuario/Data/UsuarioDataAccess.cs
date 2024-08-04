using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Data
{
    public class UsuarioDataAccess
    {
        private readonly string _connectionString;

        public UsuarioDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<UsuarioConDetalles> GetUsuariosConDetalles()
        {
            var usuariosConDetalles = new List<UsuarioConDetalles>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT 
                                u.UsuarioID,
                                u.NombreUsuario,
                                p.Nombre,
                                p.Apellido,
                                r.NombreRol,
                                u.Estado
                              FROM 
                                Usuarios u
                              JOIN 
                                Personas p ON u.PersonaID = p.PersonaID
                              JOIN 
                                Roles r ON u.RolID = r.RolID";

                using (var command = new SqlCommand(query, connection))
                {
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

            return usuariosConDetalles;
        }

        public Usuario GetUsuarioByID(int id)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT 
                                UsuarioID, 
                                NombreUsuario, 
                                PersonaID, 
                                RolID, 
                                Contraseña, 
                                FechaCambio, 
                                Estado, 
                                CreadoPor, 
                                ActualizadoPor
                              FROM Usuarios 
                              WHERE UsuarioID = @UsuarioID";

                using (var command = new SqlCommand(query, connection))
                {
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
                                Contraseña = reader["Contraseña"].ToString(),
                                FechaCambio = Convert.ToDateTime(reader["FechaCambio"]),
                                Estado = Convert.ToInt32(reader["Estado"]),
                                CreadoPor = reader["CreadoPor"].ToString(),
                                ActualizadoPor = reader["ActualizadoPor"].ToString()
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public void UpdateUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"UPDATE Usuarios 
                              SET 
                                NombreUsuario = @NombreUsuario,
                                PersonaID = @PersonaID,
                                RolID = @RolID,
                                Contraseña = @Contraseña,
                                Estado = @Estado,
                                CreadoPor = @CreadoPor  
                              WHERE UsuarioID = @UsuarioID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@PersonaID", usuario.PersonaID);
                    command.Parameters.AddWithValue("@RolID", usuario.RolID);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);
                    command.Parameters.AddWithValue("@CreadoPor", usuario.CreadoPor);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public UsuarioConDetalles GetUsuarioConDetallesByID(int id)
        {
            UsuarioConDetalles usuarioConDetalles = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT 
                                u.UsuarioID,
                                u.NombreUsuario,
                                p.Nombre,
                                p.Apellido,
                                r.NombreRol,
                                u.Estado,
                                u.Contraseña
                              FROM 
                                Usuarios u
                              JOIN 
                                Personas p ON u.PersonaID = p.PersonaID
                              JOIN 
                                Roles r ON u.RolID = r.RolID
                              WHERE u.UsuarioID = @UsuarioID";

                using (var command = new SqlCommand(query, connection))
                {
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

            return usuarioConDetalles;
        }

        public void InsertUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO Usuarios 
                                (NombreUsuario, PersonaID, RolID, Contraseña, Estado, CreadoPor) 
                              VALUES 
                                (@NombreUsuario, @PersonaID, @RolID, @Contraseña, @Estado, @CreadoPor)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@PersonaID", usuario.PersonaID);
                    command.Parameters.AddWithValue("@RolID", usuario.RolID);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);
                    command.Parameters.AddWithValue("@CreadoPor", usuario.CreadoPor);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
