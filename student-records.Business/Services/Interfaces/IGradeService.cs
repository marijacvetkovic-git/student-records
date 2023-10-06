using student_records.Business.DTOs.Grade;

namespace student_records.Business.Services.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDTO> Add(GradeCreateDTO gradeCreateDTO);
        Task<double> AverageGradeForSubject(int subjectId);
        Task<double> AverageGradeForStudentsOfYearOfStudies(int yearOfStudies);
        Task<GradeDTO> Delete(int gradeId);
        Task<GradeDTO> Update(GradeUpdateDTO gradeUpdateDTO);
    }
}