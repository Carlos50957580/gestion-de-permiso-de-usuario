using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class Login : Controller
    {
        static string cadena = "Data sorce=(local);Initial Catalog = usuarios; Integrated Security = true";
        public IActionResult acceder()
        {
            return View();
        }

        public IActionResult resgistrar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult resgistrar(Usuarios ousuarios)
        {
            bool registrar;
            string mensaje;

            using (SqlConnection con = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("sp_RegistroUsuario", con);
                cmd.Parameters.AddWithValue("Correo", ousuarios.Correo);
                cmd.Parameters.AddWithValue("Correo", ousuarios.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                cmd.ExecuteNonQuery();

                registrar = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;

            if (registrar)
            {
                return RedirectToAction("acceder", "Login");
            }

            else
            {
                return View();
            }
        }

        [HttpPost]

        public IActionResult acceder(Usuarios ousuarios)
        {

            using (SqlConnection con = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("ValidarUsuario", con);
                cmd.Parameters.AddWithValue("Correo", ousuarios.Correo);
                cmd.Parameters.AddWithValue("Correo", ousuarios.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                ousuarios.User_id = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (ousuarios.User_id != 0)
                {
                    return RedirectToAction("Index","Home");   
                }

                else
                {
                    ViewData["Mensaje"] = "Usuario no encontrado";
                }

            }
            return View();
        }
    }
}
