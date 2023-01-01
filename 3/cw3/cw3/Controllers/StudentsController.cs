using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsServices _studentServices;

        public StudentsController(IStudentsServices studentsServices)
        {
            _studentServices = studentsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                return Ok(await _studentServices.GetStudentsAsync());
            }
            catch (Exception e)
            { 
                return BadRequest(e);
            }
        }

        [HttpGet("{indexNumber}")]
        public async Task<IActionResult> GetStudentByIndex(string indexNumber)
        {
            Student st = await _studentServices.GetStudentByIndexAsync(indexNumber);
            if (st == null)
            {
                return NotFound();
            }

            return Ok(st);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {

            Student s = await _studentServices.UpdateStudentAsync(student);

            if (s == null)
            {
                return NotFound();
            }

            return Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                await _studentServices.AddStudent(student);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return StatusCode(201, student);
        }

        [HttpDelete("{indexNumber}")]
        public async Task<IActionResult> DeleteStudent(string indexNumber)
        {
            try
            {
                await _studentServices.DeleteStudent(indexNumber);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

    }
}
