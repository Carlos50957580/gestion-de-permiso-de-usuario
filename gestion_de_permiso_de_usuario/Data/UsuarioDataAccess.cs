using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Authorization;
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
                // Manejo de excepción: Registrar el error o lanzar una excepción
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
            }
            catch (Exception ex)
            {
                // Manejo de excepción: Registrar el error o lanzar una excepción
                throw new Exception("Error al obtener usuario por ID.", ex);
            }

            return usuario;
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
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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
            catch (Exception ex)
            {
                // Manejo de excepción: Registrar el error o lanzar una excepción
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
                // Manejo de excepción: Registrar el error o lanzar una excepción
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
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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
            catch (Exception ex)
            {
                // Manejo de excepción: Registrar el error o lanzar una excepción
                throw new Exception("Error al insertar usuario.", ex);
            }
        }
    }
}
