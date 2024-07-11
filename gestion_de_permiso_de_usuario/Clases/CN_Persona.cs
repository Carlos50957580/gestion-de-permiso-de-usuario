using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
namespace gestion_de_permiso_de_usuario.Clases
{
    public class CN_Persona
    {

            private readonly PersonaData _objPersona;

            public CN_Persona(PersonaData objPersona)
            {
                _objPersona = objPersona;
            }

            public List<People> GetPeople()
        {
                return _objPersona.GetPeople();
            }
        }
}
