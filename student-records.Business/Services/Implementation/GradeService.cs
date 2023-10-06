using AutoMapper;
using student_records.Business.DTOs.Grade;
using student_records.Business.Services.Interfaces;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;
using student_records.Business.Middleware.Exceptions;

namespace student_records.Business.Services.Implementation
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GradeService(
            IGradeRepository gradeRepository, 
            IMapper mapper, 
            IStudentRepository studentRepository, 
            ISubjectRepository subjectRepository)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }
        public async Task<GradeDTO> Add(GradeCreateDTO gradeCreateDTO)
        {
            Student student = await _studentRepository.GetById(gradeCreateDTO.StudentId);
            Subject subject = await _subjectRepository.GetById(gradeCreateDTO.SubjectId);

            student.ObjectNotNull($"Student with id {gradeCreateDTO.StudentId}");
            subject.ObjectNotNull($"Subject with id {gradeCreateDTO.SubjectId}");
            gradeCreateDTO.Value.ValueNotAcceptable( $"Grade with value {gradeCreateDTO.Value}");

            bool grade = await _gradeRepository.StudentPassedTheExam(student, subject);
            ApiExceptionHandler.GradeAlreadyExist(grade,$"Student with {student.Id}");
        
            return _mapper.Map<GradeDTO>(await _gradeRepository.Add(student,subject, gradeCreateDTO.Value));
        }

        public async Task<GradeDTO> Update(GradeUpdateDTO gradeUpdateDTO)
        {
            Grade grade = await _gradeRepository.GetById(gradeUpdateDTO.Id);
            grade.ObjectNotNull($"Grade with id {gradeUpdateDTO.Id}");
            gradeUpdateDTO.Value.ValueNotAcceptable($"Grade with value {gradeUpdateDTO.Value}");

            return _mapper.Map<GradeDTO>(await _gradeRepository.Update(grade, gradeUpdateDTO.Value));
        }
        public async Task<GradeDTO> Delete(int gradeId)
        {
            Grade grade = await _gradeRepository.GetById(gradeId);
            grade.ObjectNotNull($"Grade with id {gradeId}");

            return _mapper.Map<GradeDTO>(await _gradeRepository.Delete(grade));
        }
        public async Task<double> AverageGradeForSubject(int subjectId)
        {
            Subject subject = await _subjectRepository.GetById(subjectId);
            subject.ObjectNotNull($"Subject with id {subjectId}");
            
            List<Grade> listOfGrades= await _gradeRepository.AverageGradeForSubject(subject);
            double average = 0;
            
            foreach (Grade grade in listOfGrades)
            {
                average += grade.Value;
            }

            return average / listOfGrades.Count();
        }
        public async Task<double> AverageGradeForStudentsOfYearOfStudies(int yearOfStudies)
        {
            ApiExceptionHandler.YearOfStudiesBoundaryException(yearOfStudies, $"Year of studies {yearOfStudies}");

            List<Student> students= await _gradeRepository.GetListOfStudentsInYearOfStudies(yearOfStudies);
            double? average = 0;
            
            foreach (Student s in students)
            {
                average += s.CurrentAvarageRate;
            }

            return (double)average / students.Count();
        }
    }
}
