using AutoMapper;
using student_records.Business.DTOs;
using student_records.Business.DTOs.Grade;
using student_records.Business.DTOs.JWT;
using student_records.Business.DTOs.Student;
using student_records.Business.DTOs.Subject;
using student_records.Business.DTOs.User;
using students_records.Data.Models;

namespace student_records.Business.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<User, JWTCreateDTO>().ReverseMap();
            CreateMap<User, JWTResultsDTO>().ReverseMap();

            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentCreateDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();

            CreateMap<Subject, SubjectCreateDTO>().ReverseMap();
            CreateMap<Subject, SubjectsDTO>().ReverseMap();
            CreateMap<Subject, SubjectUpdateDTO>().ReverseMap();

            CreateMap<Grade, GradeDTO>().ReverseMap();
            CreateMap<Grade, GradeCreateDTO>().ReverseMap();
            CreateMap<Grade, GradeUpdateDTO>().ReverseMap();
        }
    }
}
