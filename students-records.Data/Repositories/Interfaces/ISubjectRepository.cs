using students_records.Data.Models;

namespace students_records.Data.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> Add(Subject subject);
        Task<bool> Delete(int id);
        Task<List<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<List<Subject>> GetByType(string type);
        Task<Subject> Update(Subject subject);
    }
}