using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace gestion_de_permiso_de_usuario.Controllers

{
    public class UserController : Controller
    {
        private readonly DatabaseService _databaseService;

        public UserController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IActionResult Index()
        {
            List<User> entities = _databaseService.GetUser();
            return View(entities);
        }


    }

}

