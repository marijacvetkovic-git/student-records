using students_records.Data.Repositories.Interfaces;
using AutoMapper;
using student_records.Business.Services.Interfaces;
using students_records.Data.Models;
using student_records.Business.DTOs;
using student_records.Business.DTOs.Subject;
using student_records.Business.Middleware.Exceptions;

namespace student_records.Business.Services.Implementation
{
    public class SubjectService:ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        
        public async Task<List<SubjectsDTO>> GetAll()
        {
            return _mapper.Map<List<SubjectsDTO>>(await _subjectRepository.GetAll());
        }

        public async Task<SubjectsDTO> Add(SubjectCreateDTO subjectDTO)
        {
            return _mapper.Map<SubjectsDTO>(await _subjectRepository.Add(_mapper.Map<Subject>(subjectDTO)));
        }

        public async Task<SubjectsDTO> GetById(int id)
        {
            Subject subject = await _subjectRepository.GetById(id);
            subject.ObjectNotNull($"Subject with id {id}");

            return _mapper.Map<SubjectsDTO>(subject);
        }

        public async Task<SubjectsDTO> Update(SubjectUpdateDTO subjectUpdateDTO, int id)
        {
            Subject subject = await _subjectRepository.GetById(id);
            
            foreach (var updateProperty in typeof(SubjectUpdateDTO).GetProperties())
            {
                if (updateProperty.GetValue(subjectUpdateDTO) == null)
                {
                    var currentProperty = subject.GetType().GetProperty(updateProperty.Name).GetValue(subject, null);
                    updateProperty.SetValue(subjectUpdateDTO, currentProperty, null);
                }
            }

            return _mapper.Map<SubjectsDTO>(await _subjectRepository.Update(subject));
        }

        public async Task<SubjectsDTO> Delete(int id)
        {
            return _mapper.Map<SubjectsDTO>(await _subjectRepository.Delete(id));
        }

        public async Task<List<SubjectsDTO>> GetByType(string type)
        {
            ApiExceptionHandler.TypeError(type, $"Type {type} ");

            return _mapper.Map<List<SubjectsDTO>>(await _subjectRepository.GetByType(type));
        }
    }
}
