using student_records.Business.DTOs;
using student_records.Business.DTOs.Subject;

namespace student_records.Business.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<SubjectsDTO> Add(SubjectCreateDTO subjectDTO);
        Task<SubjectsDTO> Delete(int id);
        Task<List<SubjectsDTO>> GetAll();
        Task<SubjectsDTO> GetById(int id);
        Task<List<SubjectsDTO>> GetByType(string type);
        Task<SubjectsDTO> Update(SubjectUpdateDTO subjectUpdateDTO, int id);
    }
}