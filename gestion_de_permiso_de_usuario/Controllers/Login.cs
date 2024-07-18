using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Data.SqlClient;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class Login : Controller
    {
        private string conexion = "Server=DESKTOP-LT3JSRI;Database=Pasantes;Integrated Security=True;TrustServerCertificate=True;";

        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(Personas personas)
        {
            using SqlConnection con = new SqlConnection(conexion);
            {
                string agregarDatos = "INSERT INTO Personas(Nombre,Apellido,FechaNacimiento,Genero,Telefono,Correo) VALUES (@Nombre,@Apellido,@FechaNacimiento,@Genero,@Telefono,@Correo)";
                
                SqlCommand cmd = new SqlCommand(agregarDatos, con);
                cmd.Parameters.AddWithValue("Nombre", personas.Nombre);
                cmd.Parameters.AddWithValue("Apellido", personas.Apellido);
                cmd.Parameters.AddWithValue("FechaNacimiento", personas.FechaNacimiento);
                cmd.Parameters.AddWithValue("Genero", personas.Genero);
                cmd.Parameters.AddWithValue("Telefono", personas.Telefono);
                cmd.Parameters.AddWithValue("Correo", personas.Correo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return View();
        }
    }
}
