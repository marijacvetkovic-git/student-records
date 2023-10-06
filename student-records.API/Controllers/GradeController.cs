using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_records.Business.DTOs.Grade;
using student_records.Business.Services.Interfaces;
using student_records.Shared.Roles;

#nullable disable

namespace student_records.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [Authorize(Roles = nameof(Roles.Professor))]
        [HttpPost]
        public async Task<IActionResult> Add(GradeCreateDTO gradeCreateDTO)
        {
            return Ok(await _gradeService.Add(gradeCreateDTO));
        }

        [Authorize(Roles = nameof(Roles.Professor))]
        [HttpPut]
        public async Task<IActionResult> Update(GradeUpdateDTO gradeUpdateDTO)
        {
            return Ok(await _gradeService.Update(gradeUpdateDTO));
        }

        [Authorize(Roles = nameof(Roles.Professor))]
        [HttpDelete("{gradeId}")]
        public async Task<IActionResult> Delete(int gradeId)
        {
            return Ok(await _gradeService.Delete(gradeId));
        }

        [Authorize(Roles = nameof(Roles.Professor))]
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> AverageGradeForSubject(int subjectId)
        {
            return Ok(await _gradeService.AverageGradeForSubject(subjectId));
        }

        [Authorize(Roles = nameof(Roles.Professor))]
        [HttpGet("{yearOfStudies}")]
        public async Task<IActionResult> AverageRateForYearOfStudies(int yearOfStudies)
        {
            return Ok(await _gradeService.AverageGradeForStudentsOfYearOfStudies(yearOfStudies));
        }
    }
}
