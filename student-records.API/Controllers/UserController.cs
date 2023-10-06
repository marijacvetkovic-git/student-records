using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_records.Business.DTOs;
using student_records.Business.DTOs.User;
using student_records.Business.Services.Interfaces;
using student_records.Shared.Roles;

#nullable disable

namespace student_records.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = nameof(Roles.Administrator))]
        [HttpGet]
        public async Task<IActionResult> GetAllProfessors()
        {
            List<UserDTO> users = await _userService.GetAllProfessors();
            return Ok(users);
        }

        [Authorize(Roles = nameof(Roles.Administrator))]
        [HttpPost]
        public async Task<IActionResult> AddProfessor([FromBody] UserCreateDTO userCreateDTO)
        {
            return Ok(await _userService.AddProfessor(userCreateDTO));
        }

        [Authorize(Roles = nameof(Roles.Administrator))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessorById(int id)
        {
            return Ok(await _userService.GetByProfessorById(id));
        }

        [Authorize(Roles = nameof(Roles.Administrator))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDTO userUpdateDTO)
        {
            return Ok(await _userService.Update(id, userUpdateDTO));
        }

        [Authorize(Roles = nameof(Roles.Administrator))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _userService.Delete(id));
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            return Ok(await _userService.SignIn(signInDTO));
        }
    }
}
