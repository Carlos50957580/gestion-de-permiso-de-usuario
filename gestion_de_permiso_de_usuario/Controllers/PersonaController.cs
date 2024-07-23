using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace gestion_de_permiso_de_usuario.Controllers
{
    public class PersonaController : Controller
    {

        private readonly PersonaData _PersonaData;

        public PersonaController(PersonaData personadata)
        {
            _PersonaData = personadata;
        }

        public IActionResult Index()
        {
            List<People> Lista1 = _PersonaData.GetPeople();
            return View(Lista1);
        }

        //[HttpPost]
        //public JsonResult GuardarPersona(People objeto)
        //{
        //    object resultado;
        //    string mensaje = string.Empty;

        //    if (objeto.PersonaID == 0)
        //    {
        //        resultado = new CN_Persona().Registrar(objeto, out mensaje);
        //    }
        //    else
        //    {
        //        resultado = new CN_Persona().Editar(objeto, out mensaje);
        //    }
        //}

    }
}
