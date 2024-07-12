using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Data
{
    public class RolDataAccess
    {
        private readonly string _connectionString;

        public RolDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddRol(Rol rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Roles (NombreRol, Descripcion, Estado) VALUES (@NombreRol, @Descripcion, @Estado)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", rol.Estado);

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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Roles", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            RolID = Convert.ToInt32(reader["RolID"]),
                            NombreRol = reader["NombreRol"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Roles WHERE RolID = @RolID", conn);
                    cmd.CommandType = CommandType.Text;
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
                            Estado = Convert.ToInt32(reader["Estado"])
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

        public void UpdateRol(Rol rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Roles SET NombreRol = @NombreRol, Descripcion = @Descripcion, Estado = @Estado WHERE RolID = @RolID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NombreRol", rol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", rol.Estado);
                    cmd.Parameters.AddWithValue("@RolID", rol.RolID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el rol: " + ex.Message);
            }
        }
    }
}