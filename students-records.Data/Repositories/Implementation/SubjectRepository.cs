using Microsoft.EntityFrameworkCore;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;

namespace students_records.Data.Repositories.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentRecordContext _studentRecordContext;

        public SubjectRepository(StudentRecordContext studentRecordContext)
        {
            _studentRecordContext = studentRecordContext;
        }

        public async Task<List<Subject>> GetAll()
        {
            return await _studentRecordContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetById(int id)
        {
            return await _studentRecordContext.Subjects.FindAsync(id);
        }

        public async Task<Subject> Add(Subject subject)
        {
            _studentRecordContext.Subjects.Add(subject);
            await _studentRecordContext.SaveChangesAsync();

            return subject;
        }

        public async Task<Subject> Update(Subject subject)
        {
            _studentRecordContext.Subjects.Update(subject);
            await _studentRecordContext.SaveChangesAsync();

            return subject;
        }

        public async Task<bool> Delete(int id)
        {
            Subject subject = _studentRecordContext.Subjects.FirstOrDefault(p => p.Id == id);
            if(subject == null) { return false; }

            _studentRecordContext.Subjects.Remove(subject);
            return await _studentRecordContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Subject>> GetByType(string type)
        {
            return await _studentRecordContext.Subjects.Where(p => p.TypeOf == type).ToListAsync(); ;
        }
    }
}
