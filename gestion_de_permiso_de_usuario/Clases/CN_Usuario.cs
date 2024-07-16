using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;

namespace gestion_de_permiso_de_usuario.Clases
{
    public class CN_Usuario
    {
       
            private readonly UserData _objUsuario;

            public CN_Usuario(UserData objUsuario)
            {
                _objUsuario = objUsuario;
            }

            public List<User> GetUser()
            {
                return _objUsuario.GetUser();
            }
        }
}
