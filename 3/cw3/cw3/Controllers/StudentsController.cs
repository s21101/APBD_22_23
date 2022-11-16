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
        public StudentsController(IStudentsServices studentsServices)
        {
            _studentServices = studentsServices;
        }

        private IStudentsServices _studentServices;

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            IEnumerable<Student> students = await _studentServices.GetStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{indexNumber}")]
        public async Task<IActionResult> GetStudentByIndex(string indexNumber)
        {
            Student st = await _studentServices.GetStudentByIndexAsync(indexNumber);
            if (st == null)
            {
                return BadRequest();
            }

            return Ok(st);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {

            Student s = await _studentServices.UpdateStudentAsync(student);

            if (s == null)
            {
                return BadRequest();
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
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

    }
}
