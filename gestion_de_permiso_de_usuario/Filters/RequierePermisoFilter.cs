using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace gestion_de_permiso_de_usuario.Filters
{
    public class PermisoAttribute : TypeFilterAttribute
    {
        public PermisoAttribute(string permisos) : base(typeof(RequierePermisoFilter))
        {
            Arguments = new object[] { permisos };
        }
    }

    public class RequierePermisoFilter : IAuthorizationFilter
    {
        private readonly string[] _permisos;

        public RequierePermisoFilter(string permisos)
        {
            // Dividir los permisos por comas y eliminar espacios en blanco 
            _permisos = permisos.Split(',').Select(p => p.Trim()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;

            // Resolver UsuarioDataAccess a través de IServiceProvider 
            var usuarioDataAccess = context.HttpContext.RequestServices.GetService<UsuarioDataAccess>();

            var permisosDelUsuario = usuarioDataAccess.GetPermisosDeUsuario(userName);

            // Comprobar si el usuario tiene alguno de los permisos 
            var tienePermiso = _permisos.Any(permiso => permisosDelUsuario.Contains(permiso));

            if (!tienePermiso)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

