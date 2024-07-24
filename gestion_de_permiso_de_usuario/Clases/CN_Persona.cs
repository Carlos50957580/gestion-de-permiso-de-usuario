using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;
namespace gestion_de_permiso_de_usuario.Clases
{
    public class CN_Persona
    {

        private readonly PersonaData _objPersona;

        public CN_Persona()
        {
            _objPersona = objPersona;
        }

        public List<People> GetPeople()
        {
            return _objPersona.GetPeople();
        }

        public IActionResult Registrar(People obj)
        {
            var mensaje = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(obj.Nombre) && !string.IsNullOrWhiteSpace(obj.Apellido) && !string.IsNullOrWhiteSpace(obj.Telefono))
                {
                    mensaje = "El nombre del usuario no puede ser vacio";
                }

                return _objPersona.Registrar(obj, out mensaje);
            }
            catch (Exception ex) {
                
            }
           
        }

        public bool Editar(People obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
                return false;
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
                return false;
            }
            else if (string.IsNullOrEmpty(obj.Telefono) || string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje = "El teléfono del usuario no puede ser vacio";
                return false;
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
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
