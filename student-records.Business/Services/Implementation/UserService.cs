using AutoMapper;
using Microsoft.Extensions.Configuration;
using student_records.Business.DTOs;
using student_records.Business.DTOs.JWT;
using student_records.Business.DTOs.User;
using student_records.Business.Services.Interfaces;
using student_records.Business.TokenGenerator;
using students_records.Data.Models;
using students_records.Data.Repositories.Interfaces;
using student_records.Business.Middleware.Exceptions;
using System.Text;

namespace student_records.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ITokenGenerator tokenGenerator,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }

        public async Task<List<UserDTO>> GetAllProfessors()
        {
            return _mapper.Map<List<UserDTO>>(await _userRepository.GetAllProfessors());
        }

        public async Task<UserDTO> AddProfessor(UserCreateDTO sDTO)
        {
            return _mapper.Map<UserDTO>(await _userRepository.AddProffesor(_mapper.Map<User>(sDTO)));
        }

        public async Task<UserDTO> GetByProfessorById(int id)
        {
            User user = await _userRepository.GetProfessorById(id);
            user.ObjectNotNull($"User with id {id}");

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Update(int id, UserUpdateDTO userUpdateDTO)
        {
            User user = await _userRepository.GetProfessorById(id);
            
            foreach (var updateProperty in typeof(UserUpdateDTO).GetProperties())
            {
                if (updateProperty.GetValue(userUpdateDTO) == null)
                {
                    var currentProperty = user.GetType()?.GetProperty(updateProperty.Name)?.GetValue(user, null);
                    updateProperty.SetValue(userUpdateDTO, currentProperty, null);
                }
            }

            return _mapper.Map<UserDTO>(await _userRepository.Update(user));
        }

        public async Task<UserDTO> Delete(int id)
        {
            return _mapper.Map<UserDTO>(await _userRepository.Delete(id));
        }

        public async Task<JWTResultsDTO> SignIn(SignInDTO signInDTO)
        {
            byte[] bytePassword = Convert.FromBase64String(signInDTO.Password);
            string decodedPassword = Encoding.UTF8.GetString(bytePassword);

            User user = await _userRepository.Authenticate(signInDTO.Email, decodedPassword);
            user.ObjectNotNull($"User with id {user.Id}");

            JWTCreateDTO userToken = new();
            _mapper.Map(user, userToken);
            
            return _tokenGenerator.GenerateJWTToken(userToken, DateTime.UtcNow.AddDays(Int32.Parse(_configuration.GetSection("JWT:Duration").Value)));
        }
    }
}
