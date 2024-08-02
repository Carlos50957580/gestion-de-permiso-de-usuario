using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace gestion_de_permiso_de_usuario.Controllers
{
    public class UserController : Controller
    {

        private readonly UserData _UserData;

        public UserController(UserData userdata)
        {
            _UserData = userdata;
        }

        public IActionResult Index()
        {
            List<User> Lista = _UserData.GetUsers();
            return View(Lista);
        }

        [HttpGet]
        public IActionResult GetUsuarioById(int id)
        {
            var persona = _UserData.GetUserById(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Json(persona);
        }

        public IActionResult UpdateUsuario([FromBody] User user)
        {
            if (user.UsuarioID == 0)
            {
                return BadRequest(new { message = "Datos inválidos para la actualización." });
            }

            try
            {
                _UserData.UpdateUser(user);
                return Ok(new { message = "Usuario actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la Usuario: " + ex.Message });
            }
        }

    }
}





