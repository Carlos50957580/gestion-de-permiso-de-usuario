using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using gestion_de_permiso_de_usuario.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace gestion_de_permiso_de_usuario.Controllers;


public class UserController : Controller
{

    private readonly UserData _UserData;

    public UserController(UserData userdata)
    {
        _UserData = userdata;
    }

    public IActionResult Index()
    {
        List<User> Lista = _UserData.GetUser();
        return View(Lista);
    }


}






