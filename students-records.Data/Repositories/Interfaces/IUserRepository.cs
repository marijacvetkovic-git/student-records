using students_records.Data.Models;

namespace students_records.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddProffesor(User user);
        Task<User> Authenticate(string email, string password);
        Task<bool> Delete(int id);
        Task<List<User>> GetAllProfessors();
        Task<User> GetProfessorById(int id);
        Task<User> Update(User user);
    }
}