using students_records.Data.Models;

namespace students_records.Data.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> Add(Student student);
        Task<bool> Delete(int id);
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<Student> Update(Student student);
    }
}