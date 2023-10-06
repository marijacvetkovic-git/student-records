using student_records.Business.DTOs;
using student_records.Business.DTOs.JWT;
using student_records.Business.DTOs.User;

namespace student_records.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> AddProfessor(UserCreateDTO sDTO);
        Task<JWTResultsDTO> SignIn(SignInDTO signInDTO);
        Task<UserDTO> Delete(int id);
        Task<List<UserDTO>> GetAllProfessors();
        Task<UserDTO> GetByProfessorById(int id);
        Task<UserDTO> Update(int id, UserUpdateDTO userUpdateDTO);
    }
}