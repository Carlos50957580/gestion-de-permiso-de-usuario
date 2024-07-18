using gestion_de_permiso_de_usuario.Models;
using gestion_de_permiso_de_usuario.Services;
using System.Data.SqlClient;
using System.Data;
using NuGet.Protocol.Plugins;
namespace gestion_de_permiso_de_usuario.Data
{
    public class PersonaData : DatabaseService
    {
        public PersonaData(IConfiguration configuration) : base(configuration)
        {

        }

        public List<People> GetPeople()
        {
            List<People> Lista1 = new List<People>();

            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT PersonaID,Nombre,Apellido,FechaNacimiento,Genero,Telefono,Correo, FechaCambio FROM Personas", connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista1.Add(new People
                            {
                                PersonaID = Convert.ToInt32(reader["PersonaID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                FechaNacimiento = reader["FechaNacimiento"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                FechaCambio = reader["FechaCambio"].ToString()

                            });
                        }

                    }
                }
            }
            catch
            {
                //Esto es un herramienta sorpresa que nos ayudará más tarde, ha ha.

            }
            return Lista1;
        }

        public int Registrar(People obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPersona", connection);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(People obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", connection);
                    cmd.Parameters.AddWithValue("PersonaID", obj.PersonaID);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

            }
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from Personas where PersonaID = @id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
