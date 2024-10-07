using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using gestion_de_permiso_de_usuario.Models;

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

        // Lista de permisos 
        var permisosVerRoles = new List<string> { "Ver Roles", "Crear Roles", "Editar Roles", "Ver Detalles de roles" };

        var permisosVerPermisos = new List<string> { "Ver Permisos", "Asignar Permisos", "Crear Permisos", "Ver Detalles de Permisos", "Editar Permisos" };

        var permisosVerPersonas = new List<string> { "Ver y editar Personas" };
        //
        var permisosVerUsuarios = new List<string> { "Ver Usuarios", "Editar Usuarios", "Crear Usuarios", "Ver Detalles de Usuarios" };


        if (permisosVerRoles.Any(permiso => permisos.Contains(permiso)))
        {
            viewModel.TotalRoles = _rolDataAccess.ObtenerTotalRoles();
            viewModel.TotalRolesActivos = _rolDataAccess.ObtenerRolesActivos();
            viewModel.TotalRolesInactivos = _rolDataAccess.ObtenerRolesInactivos();
        }

        // Verificar si el usuario tiene permisos para ver permisos
        if (permisosVerPermisos.Any(permiso => permisos.Contains(permiso)))
        {
            viewModel.TotalPermisos = _permisoDataAccess.ObtenerTotalPermisos();
            viewModel.TotalPermisosActivos = _permisoDataAccess.ObtenerPermisosActivos();
            viewModel.TotalPermisosInactivos = _permisoDataAccess.ObtenerPermisosInactivos();
        }

        // Verificar si el usuario tiene permisos para ver Usuarios
        if (permisosVerUsuarios.Any(permiso => permisos.Contains(permiso)))
        {
            viewModel.TotalUsuarios = _usuarioDataAccess.ObtenerTotalUsuarios();
            viewModel.TotalUsuariosActivos = _usuarioDataAccess.ObtenerUsuariosActivos();
            viewModel.TotalUsuariosInactivos = _usuarioDataAccess.ObtenerUsuariosInactivos();
        }

        // Verificar si el usuario tiene permisos para ver personas
        if (permisosVerPersonas.Any(permiso => permisos.Contains(permiso)))
        {
            viewModel.Totalpersonas = _personaDataAccess.ObtenerTotalPersonas();
        }



        return View(viewModel);
    }
}