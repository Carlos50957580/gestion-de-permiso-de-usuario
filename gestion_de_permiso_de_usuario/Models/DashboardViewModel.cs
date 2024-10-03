public class DashboardViewModel
{
    public int TotalUsuarios { get; set; }
    public int TotalUsuariosActivos { get; set; }
    public int TotalUsuariosInactivos { get; set; }

    public int TotalRoles { get; set; }
    public int TotalRolesActivos { get; set; }
    public int TotalRolesInactivos { get; set; }

    public int TotalPermisos { get; set; }
    public int TotalPermisosActivos { get; set; }
    public int TotalPermisosInactivos { get; set; }

    public int Totalpersonas { get; set; }

    public List<UsuarioPorRol> UsuariosPorRol { get; set; } // Para el gráfico de barras
}

public class UsuarioPorRol
{
    public string RolNombre { get; set; }
    public int CantidadUsuarios { get; set; }
}
