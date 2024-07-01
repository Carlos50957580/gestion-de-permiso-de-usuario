using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace gestion_de_permiso_de_usuario.Controllers;


public class UserController : Controller
{ 
    private UserData objUsuario = new UserData();

    public List<User> GetUser()
    {
    return objUsuario.GetUser(); 
    }
}

//    private readonly DatabaseService _databaseService;

//    public UserController(DatabaseService databaseService)
//    {
//        _databaseService = databaseService;
//    }

//    public IActionResult Index()
//    {
//        List<User> Lista = _databaseService.GetUser();
//        return View(Lista);
//    }




