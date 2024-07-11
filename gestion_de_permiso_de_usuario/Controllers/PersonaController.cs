using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Data;
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


    }
}
