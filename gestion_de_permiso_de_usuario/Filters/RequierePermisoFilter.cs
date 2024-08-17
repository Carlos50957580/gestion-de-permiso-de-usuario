using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using gestion_de_permiso_de_usuario.Data;

namespace gestion_de_permiso_de_usuario.Filters
{
    public class PermisoAttribute : TypeFilterAttribute
    {
        public PermisoAttribute(string permiso) : base(typeof(RequierePermisoFilter))
        {
            Arguments = new object[] { permiso };
        }
    }

    public class RequierePermisoFilter : IAuthorizationFilter
    {
        private readonly string _permiso;

        public RequierePermisoFilter(string permiso)
        {
            _permiso = permiso;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;

            // Resolver UsuarioDataAccess a través de IServiceProvider
            var usuarioDataAccess = context.HttpContext.RequestServices.GetService<UsuarioDataAccess>();

            var permisosDelUsuario = usuarioDataAccess.GetPermisosDeUsuario(userName);

            if (!permisosDelUsuario.Contains(_permiso))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
