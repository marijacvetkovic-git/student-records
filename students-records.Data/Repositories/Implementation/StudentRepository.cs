using Microsoft.EntityFrameworkCore;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;

namespace students_records.Data.Repositories.Implementation
{
    public class StudentRepository:IStudentRepository
    {
        private readonly StudentRecordContext _studentRecordContext;
    
        public StudentRepository(StudentRecordContext studentRecordContext)
        {
            _studentRecordContext = studentRecordContext;
        }

        public async Task<List<Student>> GetAll()
        {
            return await _studentRecordContext.Students.ToListAsync();
        }

        public async Task<Student> Add(Student student)
        {
            _studentRecordContext.Students.Add(student);
            await _studentRecordContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student> GetById(int id)
        {
            return await _studentRecordContext.Students.FindAsync(id);
        }

        public async Task<Student> Update(Student student)
        {
            Student studentToUpdate =_studentRecordContext.Students.Where(p=>p.Id==student.Id).FirstOrDefault();
            _studentRecordContext.Update(studentToUpdate);
            await _studentRecordContext.SaveChangesAsync();

            return studentToUpdate;
        }

        public async Task<bool> Delete (int id)
        {
            Student student = _studentRecordContext.Students.FirstOrDefault(p=>p.Id==id);
            if (student == null) { return false; }

            _studentRecordContext.Students.Remove(student);
            return await _studentRecordContext.SaveChangesAsync() > 0;
        }
    }
}
