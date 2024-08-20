namespace gestion_de_permiso_de_usuario.Services;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DatabaseService
{
    public readonly string _connectionString;
    public SqlConnection conexion;
    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public SqlConnection GetConnection()
    {

        return conexion;
    }

    public void Conectar()
    {
        if (conexion.State != ConnectionState.Open)
        {

            conexion.Open();
        }
    }

    public void Cerrar()
    {
        if (conexion.State != ConnectionState.Closed)
        {

            conexion.Close();
        }
    }


}
