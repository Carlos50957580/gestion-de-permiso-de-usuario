using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

public class DashboardController : Controller
{
    private readonly PermisoDataAccess _permisoDataAccess;
    private readonly UsuarioDataAccess _usuarioDataAccess;
    private readonly RolDataAccess _rolDataAccess;
    private readonly PersonaDataAccess _personaDataAccess;
    private readonly IConfiguration _configuration;

    public DashboardController(IConfiguration configuration)
    {
        _configuration = configuration;
        _permisoDataAccess = new PermisoDataAccess(_configuration);
        _usuarioDataAccess = new UsuarioDataAccess(_configuration);
        _rolDataAccess = new RolDataAccess(_configuration);
        _personaDataAccess = new PersonaDataAccess(_configuration);
    }

    public ActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");

        if (userId == null)
        {
            return RedirectToAction("Iniciar", "Login");
        }

        var permisos = _permisoDataAccess.ObtenerPermisosPorUsuario((int)userId);
        var viewModel = new DashboardViewModel();

        if (permisos.Contains("Ver Usuarios")) //
        {
            viewModel.TotalUsuarios = _usuarioDataAccess.ObtenerTotalUsuarios();
            viewModel.TotalUsuariosActivos = _usuarioDataAccess.ObtenerUsuariosActivos();
            viewModel.TotalUsuariosInactivos = _usuarioDataAccess.ObtenerUsuariosInactivos();

            viewModel.UsuariosPorRol = _usuarioDataAccess.ObtenerCantidadUsuariosPorRol();
        }

        if (permisos.Contains("Ver Roles")) //
        {
            viewModel.TotalRoles = _rolDataAccess.ObtenerTotalRoles();
            viewModel.TotalRolesActivos = _rolDataAccess.ObtenerRolesActivos();
            viewModel.TotalRolesInactivos = _rolDataAccess.ObtenerRolesInactivos();
        }

        if (permisos.Contains("Ver Permisos")) //
        {
            viewModel.TotalPermisos = _permisoDataAccess.ObtenerTotalPermisos();
            viewModel.TotalPermisosActivos = _permisoDataAccess.ObtenerPermisosActivos();
            viewModel.TotalPermisosInactivos = _permisoDataAccess.ObtenerPermisosInactivos();
        }

     
        if (permisos.Contains("Ver y editar Personas"))
        {
            viewModel.Totalpersonas = _personaDataAccess.ObtenerTotalPersonas();
        }

        ////
        //if (permisos.Contains("Ver Personas"))
        //{
        //    viewModel.Totalpersonas = _permisoDataAccess.ObtenerTotalPermisos();
        //}

        return View(viewModel);
    }
}