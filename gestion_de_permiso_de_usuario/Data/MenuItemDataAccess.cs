using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

public class MenuItemDataAccess
{
    private readonly string _connectionString;

    public MenuItemDataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<MenuItem> GetMenuItems()
    {
        List<MenuItem> menuItems = new List<MenuItem>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT MenuItemID, Descripcion, Controlador, Accion, Icono, ParentID FROM MenuItemsNew", conn);
            cmd.CommandType = CommandType.Text;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                menuItems.Add(new MenuItem
                {
                    MenuItemID = Convert.ToInt32(reader["MenuItemID"]),
                    Descripcion = reader["Descripcion"].ToString(),
                    Controlador = reader["Controlador"].ToString(),
                    Accion = reader["Accion"].ToString(),
                    Icono = reader["Icono"].ToString(),
                    ParentID = reader["ParentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ParentID"])
                });
            }
        }

        return menuItems;
    }
}
