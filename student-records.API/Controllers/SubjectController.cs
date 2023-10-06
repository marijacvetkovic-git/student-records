using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_records.Business.DTOs;
using student_records.Business.DTOs.Subject;
using student_records.Business.Services.Interfaces;
#nullable disable

namespace student_records.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subjectService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _subjectService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubjectCreateDTO subjectDTO)
        {
            return Ok(await _subjectService.Add(subjectDTO));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubjectUpdateDTO studentUpdateDTO)
        {
            return Ok(await _subjectService.Update(studentUpdateDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _subjectService.Delete(id));
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            return Ok(await _subjectService.GetByType(type));
        }
    }
}
