using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gestion_de_permiso_de_usuario.Models;
using Microsoft.Extensions.Configuration;

namespace gestion_de_permiso_de_usuario.Data
{
    public class PersonaDataAccess
    {
        private readonly string _connectionString;

        public PersonaDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //public void AddPersona(Persona persona)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("INSERT INTO Personas (Nombre, Apellido, FechaNacimiento, Genero, Telefono, Correo, FechaCambio) VALUES (@Nombre, @Apellido, @FechaNacimiento, @Genero, @Telefono, @Correo, @FechaCambio)", conn);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
        //            cmd.Parameters.AddWithValue("@Apellido", persona.Apellido);
        //            cmd.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
        //            cmd.Parameters.AddWithValue("@Genero", persona.Genero);
        //            cmd.Parameters.AddWithValue("@Telefono", persona.Telefono);
        //            cmd.Parameters.AddWithValue("@Correo", persona.Correo);
        //            cmd.Parameters.AddWithValue("@FechaCambio", persona.FechaCambio);

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar la persona: " + ex.Message);
        //    }
        //}

        public List<Persona> GetPersonas()
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Personas", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        personas.Add(new Persona
                        {
                            PersonaID = Convert.ToInt32(reader["PersonaID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            Genero = reader["Genero"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            FechaCambio = Convert.ToDateTime(reader["FechaCambio"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las personas: " + ex.Message);
            }

            return personas;
        }

        public Persona GetPersonaById(int personaID)
        {
            Persona persona = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Personas WHERE PersonaID = @PersonaID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PersonaID", personaID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        persona = new Persona
                        {
                            PersonaID = Convert.ToInt32(reader["PersonaID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            Genero = reader["Genero"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            FechaCambio = Convert.ToDateTime(reader["FechaCambio"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la persona: " + ex.Message);
            }

            return persona;
        }

        public void UpdatePersona(Persona persona)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Personas SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Genero = @Genero, Telefono = @Telefono, Correo = @Correo, FechaCambio = GETDATE() WHERE PersonaID = @PersonaID", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", persona.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", persona.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", persona.Correo);
                    cmd.Parameters.AddWithValue("@PersonaID", persona.PersonaID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la persona: " + ex.Message);
            }
        }
    }
}

