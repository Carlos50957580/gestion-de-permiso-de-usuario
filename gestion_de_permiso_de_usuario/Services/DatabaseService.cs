namespace gestion_de_permiso_de_usuario.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

public class DatabaseService
{
    private readonly string _connectionString;
    private SqlConnection conexion;
    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public SqlConnection GetConnection() {
    
        return conexion;
    }

    public void Conectar()
    {
        if (conexion.State != ConnectionState.Open) {

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
