using students_records.Data.Models;

namespace students_records.Data.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Task<Grade> Add(Student student, Subject subject, int value);
        Task<List<Grade>> AverageGradeForSubject(Subject subject);
        Task<List<Student>> GetListOfStudentsInYearOfStudies(int yearOfStudies);
        Task<bool> Delete(Grade grade);
        Task<bool> StudentPassedTheExam(Student student, Subject subject);
        Task<Grade> GetById(int id);
        Task<Grade> Update(Grade grade, int value);
    }
}