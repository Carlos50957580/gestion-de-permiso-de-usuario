using Microsoft.AspNetCore.Mvc;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class Login(IConfiguration configuration) : Controller
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Iniciar()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Registrar(Persona personas)
        {
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                {
                    SqlCommand cmd = new SqlCommand("spAddPersona", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", personas.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", personas.Apellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", personas.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", personas.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", personas.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", personas.Correo);
                    cmd.Parameters.AddWithValue("@FechaCambio", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ViewData["Completado"] = "Registro Completado";
                return View();
            }
            catch (Exception ex) 
            {
                ViewData["Incompleto"] = "Registro no completado";
               return View();
            }
         
    
        }

        [HttpPost]
        public async Task<IActionResult> IniciarAsync(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ValidarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

              
                command.Parameters.AddWithValue("@NombreUsuario",usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);

                // Parámetro de salida
                SqlParameter outputParameter = new SqlParameter("@Resultado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParameter);

               
                connection.Open();
                command.ExecuteNonQuery();

             
                bool esValido = (bool)outputParameter.Value;
                if (esValido)
                {
                    // Credenciales válidas
                   // ViewData["CompletadoInicio"] = "Bienvenido";
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    // Credenciales inválidas
                    ViewData["IncompletoInicio"] = "Usuario o contraseña incorrectos";
                    return View();
                }
            }
        }
       
    }
}