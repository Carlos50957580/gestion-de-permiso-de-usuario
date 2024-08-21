using gestion_de_permiso_de_usuario.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace gestion_de_permiso_de_usuario.Controllers
{
    public class Login(IConfiguration configuration) : Controller
    {

        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the password string to a byte array.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string.
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Iniciar()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
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
                string encryptedPassword = EncryptPassword(usuario.Contraseña);

                SqlCommand command = new SqlCommand("ValidarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", encryptedPassword);

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
                    // Obtener el rol del usuario
                    SqlCommand roleCommand = new SqlCommand("ObtenerRolUsuario", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    roleCommand.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);

                    SqlParameter roleParameter = new SqlParameter("@NombreRol", SqlDbType.NVarChar, 15)
                    {
                        Direction = ParameterDirection.Output
                    };
                    roleCommand.Parameters.Add(roleParameter);

                    roleCommand.ExecuteNonQuery();
                    string rolUsuario = (string)roleParameter.Value;

                    // Crear las claims
                    var claims = new List<Claim>
                     {
                       new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                       new Claim(ClaimTypes.Role, rolUsuario)
                     };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // Credenciales inválidas
                    ViewData["IncompletoInicio"] = "Usuario o contraseña incorrectos";
                    return View();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Cierra la sesión del usuario
            await HttpContext.SignOutAsync();

            // Redirige al usuario a la página de inicio o a cualquier otra página después del logout
            return RedirectToAction("Iniciar", "Login");
        }
    }
}