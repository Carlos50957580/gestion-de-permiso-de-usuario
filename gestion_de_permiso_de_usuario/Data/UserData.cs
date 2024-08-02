using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Data
{
    public class UserDataAccess
    {
        private readonly string _connectionString;

        public UserDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (NombreUsuario, PersonaID, RolID, Contraseña, FechaCambio, Estado, CreadoPor, ActualizadoPor) VALUES (@NombreUsuario, @PersonaID, @RolID, @Contraseña, @FechaCambio, @Estado, @CreadoPor, @ActualizadoPor)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NombreUsuario", user.NombreUsuario);
                    cmd.Parameters.AddWithValue("@PersonaID", user.PersonaID);
                    cmd.Parameters.AddWithValue("@RolID", user.RolID);
                    cmd.Parameters.AddWithValue("@Contraseña", user.Contraseña);
                    cmd.Parameters.AddWithValue("@FechaCambio", user.FechaCambio);
                    cmd.Parameters.AddWithValue("@Estado", user.Estado);
                    cmd.Parameters.AddWithValue("@CreadoPor", user.CreadoPor);
                    cmd.Parameters.AddWithValue("@ActualizadoPor", user.ActualizadoPor);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            PersonaID = Convert.ToInt32(reader["PersonaID"]),
                            RolID = Convert.ToInt32(reader["RolID"]),
                            Contraseña = reader["Contraseña"].ToString(),
                            FechaCambio = reader["FechaCambio"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            ActualizadoPor = reader["ActualizadoPor"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE UsuarioID = @UsuarioID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UsuarioID", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            PersonaID = Convert.ToInt32(reader["PersonaID"]),
                            RolID = Convert.ToInt32(reader["RolID"]),
                            Contraseña = reader["Contraseña"].ToString(),
                            FechaCambio = reader["FechaCambio"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            CreadoPor = reader["CreadoPor"].ToString(),
                            ActualizadoPor = reader["ActualizadoPor"].ToString(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET NombreUsuario = @NombreUsuario, PersonaID = @PersonaID, RolID = @RolID, Contraseña = @Contraseña, FechaCambio = GETDATE(), Estado = @Estado, ActualizadoPor = @ActualizadoPor WHERE UsuarioID = @UsuarioID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NombreUsuario", user.NombreUsuario);
                    cmd.Parameters.AddWithValue("@PersonaID", user.PersonaID);
                    cmd.Parameters.AddWithValue("@RolID", user.RolID);
                    cmd.Parameters.AddWithValue("@Contraseña", user.Contraseña);
                    cmd.Parameters.AddWithValue("@Estado", user.Estado);
                    cmd.Parameters.AddWithValue("@ActualizadoPor", user.ActualizadoPor);
                    cmd.Parameters.AddWithValue("@UsuarioID", user.UsuarioID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }
    }
}
