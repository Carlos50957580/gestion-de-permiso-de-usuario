using gestion_de_permiso_de_usuario.Models;

namespace gestion_de_permiso_de_usuario.Services
{
    public interface IPersonaDataAccess
    {
        IEnumerable<Persona> GetAllPersonas();
        Persona GetPersonaById(int id);
        void AddPersona(Persona persona);
        void UpdatePersona(Persona persona);
        void DeletePersona(int id);
    }
}
