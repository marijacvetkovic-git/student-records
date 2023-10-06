using AutoMapper;
using student_records.Business.Middleware.Exceptions;
using student_records.Business.DTOs;
using student_records.Business.DTOs.Student;
using student_records.Business.Services.Interfaces;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;
#nullable disable

namespace student_records.Business.Services.Implementation
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentDTO>> GetAll() 
        {
            return _mapper.Map<List<StudentDTO>>(await _studentRepository.GetAll());
        }

        public async Task<StudentDTO>Add(StudentCreateDTO studentCreateDTO)
        {
            ApiExceptionHandler.YearOfStudiesBoundaryException(studentCreateDTO.YearOfStudies, $"Year of studies {studentCreateDTO.YearOfStudies}");
            ApiExceptionHandler.InvalidDateTime(studentCreateDTO.DateOfBirth, $"Date of birth {studentCreateDTO.DateOfBirth}");
            ApiExceptionHandler.InvalidIndexNumber(studentCreateDTO.IndexNumber, $"Index number {studentCreateDTO.IndexNumber}");
            ApiExceptionHandler.InvalidJMBG(studentCreateDTO.Jmbg, $"JMBG {studentCreateDTO.Jmbg}");
            ApiExceptionHandler.InvalidTelephoneNumber(studentCreateDTO.TelephoneNumber, $"Telephone number {studentCreateDTO.TelephoneNumber}");
            ApiExceptionHandler.InvalidNumberOfCharacters(studentCreateDTO.FirstName, $"First name {studentCreateDTO.FirstName}");
            ApiExceptionHandler.InvalidNumberOfCharacters(studentCreateDTO.FirstName, $"Last name {studentCreateDTO.LastName}");
            ApiExceptionHandler.InvalidNumberOfCharacters(studentCreateDTO.ParentName, $"Parent name {studentCreateDTO.ParentName}");

            return _mapper.Map<StudentDTO>(await _studentRepository.Add(_mapper.Map<Student>(studentCreateDTO)));
        }

        public async Task<StudentDTO> GetById(int id)
        {
            Student student = await _studentRepository.GetById(id);
            student.ObjectNotNull($"Student with id {id}");

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> Update(StudentUpdateDTO studentUpdateDTO, int id)
        { 
            Student student = await  _studentRepository.GetById(id);
            student.ObjectNotNull($"Student with id {id}");
            
            foreach (var updateProperty in typeof(StudentUpdateDTO).GetProperties())
            {
                if (updateProperty.GetValue(studentUpdateDTO) == null)
                {
                    var currentProperty = student.GetType().GetProperty(updateProperty.Name).GetValue(student, null);
                    updateProperty.SetValue(studentUpdateDTO, currentProperty, null);
                }
            }

            return _mapper.Map<StudentDTO>(await _studentRepository.Update(student));
        }

        public async Task<StudentDTO> Delete(int id)
        {
            return _mapper.Map<StudentDTO>(await _studentRepository.Delete(id));
        }
    }
}
