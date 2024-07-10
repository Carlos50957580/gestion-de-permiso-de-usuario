using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Data
{
    public class PermisoDataAccess
    {
        private readonly string _connectionString;

        public PermisoDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddPermiso(Permiso permiso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Permisos (NombrePermiso, Descripcion, Estado, CreadoPor, FechaCambio) VALUES (@NombrePermiso, @Descripcion, @Estado, @CreadoPor, @FechaCambio)", conn);
                    cmd.CommandType = CommandType.Text;
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


        public List<Permiso> GetPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos", conn);
                    cmd.CommandType = CommandType.Text;
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

        public Permiso GetPermisoById(int permisoID)
        {
            Permiso permiso = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos WHERE PermisoID = @PermisoID", conn);
                    cmd.CommandType = CommandType.Text;
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

        public void UpdatePermiso(Permiso permiso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Permisos SET NombrePermiso = @NombrePermiso, Descripcion = @Descripcion, Estado = @Estado, FechaCambio = GETDATE(), ActualizadoPor = @ActualizadoPor WHERE PermisoID = @PermisoID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);
                    cmd.Parameters.AddWithValue("@Descripcion", permiso.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", permiso.Estado);
                    cmd.Parameters.AddWithValue("@ActualizadoPor", permiso.ActualizadoPor);
                    cmd.Parameters.AddWithValue("@PermisoID", permiso.PermisoID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el permiso: " + ex.Message);
            }
        }
    }
}
