using Microsoft.EntityFrameworkCore;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;
using System.Transactions;

namespace students_records.Data.Repositories.Implementation
{
    public class GradeRepository: IGradeRepository
    {
        private readonly StudentRecordContext _studentRecordContext;

        public GradeRepository(StudentRecordContext studentRecordContext)
        {
              _studentRecordContext = studentRecordContext;
        }

        public async Task<Grade> Add(Student student, Subject subject, int value)
        {
            Grade grade = new()
            {
                Student = student,
                Value = value,
                Subject = subject,
                DateOfGrading = DateTime.Now,
                SubjectId = subject.Id,
                StudentId = student.Id
            };

            _studentRecordContext.Grades.Add(grade);
            await _studentRecordContext.SaveChangesAsync();

            double avgRate = UpdateAverageRate(student);
            student.CurrentAvarageRate = avgRate;
            student.CurrentEspb += subject.Espb;

            _studentRecordContext.Students.Update(student);
            await _studentRecordContext.SaveChangesAsync();

            return grade;
        }

        public async Task<Grade> GetById(int id)
        {       
            return await _studentRecordContext.Grades.FindAsync(id);
        }

        public async Task<Grade> Update(Grade grade, int value)
        {
            grade.Value = value;
            _studentRecordContext.Update(grade);
            await _studentRecordContext.SaveChangesAsync();

            Student student = _studentRecordContext.Students.FirstOrDefault(p => p.Id == grade.StudentId);
            double avgRate = UpdateAverageRate(student);
            student.CurrentAvarageRate=avgRate;

            _studentRecordContext.Update(student);
            await _studentRecordContext.SaveChangesAsync();

            return grade;
        }

        public async Task<bool> StudentPassedTheExam(Student student, Subject subject )
        {
            Grade grade = await _studentRecordContext.Grades.Where(p=>p.Student==student && p.Subject==subject).FirstOrDefaultAsync();
            if (grade == null) { return false; }
           
            return true;
        }

        public async Task<bool> Delete(Grade grade)
        {
            using (var scope = new TransactionScope(
                      TransactionScopeOption.Required,
                      new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                      TransactionScopeAsyncFlowOption.Enabled))
                try
                {
                    _studentRecordContext.Grades.Remove(grade);
                    await _studentRecordContext.SaveChangesAsync();

                    Student student = _studentRecordContext.Students.FirstOrDefault(p => p.Id == grade.StudentId);
                    int espb = _studentRecordContext.Subjects.FirstOrDefault(p => p.Id == grade.SubjectId).Espb;

                    student.CurrentEspb -= espb;
                    double avgRate = UpdateAverageRate(student);
                    student.CurrentAvarageRate = avgRate;

                    await UpdateStudent(student);
                    
                    scope.Complete();
                }
                catch { throw; }

            return true;
        }

        public async Task<List<Grade>> AverageGradeForSubject(Subject subject)
        {
            return await _studentRecordContext.Grades.Where(p => p.Subject == subject).ToListAsync();
        }
        
        public async Task<List<Student>> GetListOfStudentsInYearOfStudies(int yearOfStudies)
        {
            return await _studentRecordContext.Students.Where(p => p.YearOfStudies == yearOfStudies).ToListAsync();
        }

        #region Private Methodes

        private async Task<bool> UpdateStudent(Student student)
        {
            _studentRecordContext.Students.Update(student);
            
            return await _studentRecordContext.SaveChangesAsync() > 0;
        }

        private double UpdateAverageRate(Student student)
        {
            double avgRate = 0;
            List<Grade> grades = _studentRecordContext.Grades.Where(p => p.Student == student).ToList();

            foreach (var grade in grades)
            {
                avgRate += grade.Value;
            }

            return avgRate / _studentRecordContext.Grades.Where(p => p.Student == student).Count();
        }

        #endregion Private Methodes
    }
}
