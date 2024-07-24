using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace gestion_de_permiso_de_usuario.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaData _objPersona;
        private readonly PersonaData _PersonaData;
        private readonly CN_Persona _cnPersona;


        public PersonaController(PersonaData personadata, CN_Persona cnPersona)
        {
            _PersonaData = personadata;
            _cnPersona = cnPersona;
        }

        public IActionResult Index()
        {
            List<People> Lista1 = _PersonaData.GetPeople();
            return View(Lista1);
        }

        public JsonResult ListaPersonas()
        {
            List<People> pLista = _cnPersona.GetPeople();
            return Json(new { data = pLista });
        }

        [HttpPost]
        public JsonResult GuardarPersona(People objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.PersonaID == 0)
            {
                resultado = _cnPersona.Registrar(objeto);
            }
            else
            {
                resultado = _cnPersona.Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

    }
}
