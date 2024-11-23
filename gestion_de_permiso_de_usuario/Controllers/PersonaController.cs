using gestion_de_permiso_de_usuario.Data;
using gestion_de_permiso_de_usuario.Filters;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestion_de_permiso_de_usuario.Controllers
{
    [Permiso("Ver y editar Personas")]
    public class PersonaController : Controller
    {
        private readonly PersonaDataAccess _personaDataAccess;

        public PersonaController(IConfiguration configuration)
        {
            _personaDataAccess = new PersonaDataAccess(configuration);
        }

        public IActionResult Index()
        {
            var personas = _personaDataAccess.GetPersonas();
            //Obtener el nombre del usuario autenticado
            var userName = User.Identity.Name;

            // Pasar el nombre del usuario a la vista a través del ViewBag
            ViewBag.UserName = userName;
            return View(personas);
        }

        // GET: Persona/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _personaDataAccess.GetPersonaById(id.Value);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Persona/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PersonaID,Nombre,Cedula,Apellido,FechaNacimiento,Genero,Telefono,Correo,FechaCambio")] Persona persona)
        {
            if (id != persona.PersonaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _personaDataAccess.UpdatePersona(persona);
                    return Json(persona); /*RedirectToAction(nameof(Index));*/
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la persona: " + ex.Message);
                    return View(persona);
                }
            }
            return View(persona);
        }

        [HttpGet]
        public IActionResult GetPersonaById(int id)
        {
            var persona = _personaDataAccess.GetPersonaById(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Json(persona);
        }

        [HttpPost]
        public IActionResult UpdatePersona([FromBody] Persona persona)
        {
            if (persona.PersonaID == 0)
            {
                return BadRequest(new { message = "Datos inválidos para la actualización." });
            }

            try
            {
                _personaDataAccess.UpdatePersona(persona);
                return Ok(new { message = "Persona actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la persona: " + ex.Message });
            }
        }
    }
}