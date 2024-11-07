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

    public List<MenuItem> GetMenuItems(int userId)
    {
        List<MenuItem> menuItems = new List<MenuItem>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT DISTINCT mi.MenuItemID, mi.Descripcion, mi.Controlador, mi.Accion, mi.Icono, mi.ParentID
                FROM MenuItemsNew mi
                LEFT JOIN MenuItemPermisos mip ON mi.MenuItemID = mip.MenuItemID
                LEFT JOIN Permisos p ON mip.PermisoID = p.PermisoID
                LEFT JOIN RolPermisos rp ON p.PermisoID = rp.PermisoID
                LEFT JOIN Roles r ON rp.RolID = r.RolID
                LEFT JOIN Usuarios u ON u.RolID = r.RolID AND u.UsuarioID = @UserId
                WHERE mi.Descripcion IN ('Registrar', 'Salir') OR u.UsuarioID = @UserId;
                ";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
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
