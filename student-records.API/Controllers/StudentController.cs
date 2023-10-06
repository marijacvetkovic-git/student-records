using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_records.Business.DTOs;
using student_records.Business.DTOs.Student;
using student_records.Business.Services.Interfaces;

#nullable disable

namespace student_records.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentCreateDTO studentDTO)
        {
            return Ok(await _studentService.Add(studentDTO));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studentService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDTO studentUpdateDTO)
        {
            return Ok(await _studentService.Update(studentUpdateDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _studentService.Delete(id));
        }
    }
}
