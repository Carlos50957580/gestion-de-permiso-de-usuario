using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class Login : Controller
    {
        private string conexion = "Server=TAPIA\\SQLEXPRESS;Database=usuarios1;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;";

        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(Persona personas)
        {
            using SqlConnection con = new SqlConnection(conexion);
            {
               
                SqlCommand cmd = new SqlCommand("spAddPersona", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", personas.Nombre);
                cmd.Parameters.AddWithValue("Apellido", personas.Apellido);
                cmd.Parameters.AddWithValue("FechaNacimiento", personas.FechaNacimiento);
                cmd.Parameters.AddWithValue("Genero", personas.Genero);
                cmd.Parameters.AddWithValue("Telefono", personas.Telefono);
                cmd.Parameters.AddWithValue("Correo", personas.Correo);
                cmd.Parameters.AddWithValue("FechaCambio", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return View();
        }
    }
}
