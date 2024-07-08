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
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Permisos (NombrePermiso, Descripcion, Estado, CreadoPor, FechaCambio, ActualizadoPor) VALUES (@NombrePermiso, @Descripcion, @Estado, @CreadoPor, @FechaCambio, @ActualizadoPor)", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NombrePermiso", permiso.NombrePermiso);
                cmd.Parameters.AddWithValue("@Descripcion", permiso.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", permiso.Estado);
                cmd.Parameters.AddWithValue("@CreadoPor", permiso.CreadoPor);
                cmd.Parameters.AddWithValue("@FechaCambio", permiso.FechaCambio);
                cmd.Parameters.AddWithValue("@ActualizadoPor", permiso.ActualizadoPor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Permiso> GetPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

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

            return permisos;
        }
    }
}
