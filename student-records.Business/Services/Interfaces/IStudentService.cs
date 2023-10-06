using student_records.Business.DTOs;
using student_records.Business.DTOs.Student;

namespace student_records.Business.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> Add(StudentCreateDTO sDTO);
        Task<StudentDTO> Delete(int id);
        Task<List<StudentDTO>> GetAll();
        Task<StudentDTO> GetById(int id);
        Task<StudentDTO> Update(StudentUpdateDTO studentUpdateDTO,int id);
    }
}