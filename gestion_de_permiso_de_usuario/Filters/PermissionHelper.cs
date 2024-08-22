using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using gestion_de_permiso_de_usuario.Data;
using System.Linq;

namespace gestion_de_permiso_de_usuario.Filters
{
    public static class PermissionHelper
    {
        // metodo para verificar si el usuario tiene al menos uno de los permisos especificados
        public static bool HasAnyPermission(HttpContext context, params string[] permissions)
        {
            var userName = context.User.Identity.Name;

            if (string.IsNullOrEmpty(userName) || permissions == null || !permissions.Any())
                return false;

            var usuarioDataAccess = context.RequestServices.GetService<UsuarioDataAccess>();
            var permisosDelUsuario = usuarioDataAccess.GetPermisosDeUsuario(userName);

            return permissions.Any(permission => permisosDelUsuario.Contains(permission));
        }
    }
}
