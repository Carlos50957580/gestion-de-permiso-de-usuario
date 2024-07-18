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

            public int Registrar(People obj, out string Mensaje)
            {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
               Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if(string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
               Mensaje = "El apellido del usario no puede ser vacio";
            }
            else if(string.IsNullOrEmpty(obj.Telefono) || string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }
            //Agrega otro campo que no pueda ir vacio si quieres

            if (string.IsNullOrEmpty(Mensaje))
            {

                //Para aplicar Encriptacion y otras logica mas adelante

                return _objPersona.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }  
      }

        public bool Editar(People obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Telefono) || string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {

                //Para aplicar Encriptacion y otras logica mas adelante

                return _objPersona.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return _objPersona.Eliminar(id, out Mensaje);
        }
        }
}
