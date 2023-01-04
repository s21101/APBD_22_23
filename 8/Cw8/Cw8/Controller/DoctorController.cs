using Cw8.DTO.Request;
using Cw8.DTO.Responce;
using Cw8.Models;
using Cw8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw8.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            return Ok(await _doctorService.GetDoctorsAsync());
        }

        [HttpGet("{idDoctor}")]
        public async Task<IActionResult> GetDoctorAsync(int idDoctor)
        {
            DoctorDTO res = await _doctorService.GetDoctorAsync(idDoctor);

            if (res != null)
            {
                return Ok(res);
            }
            else 
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(DoctorDTOREQ newDoctor)
        {

            await _doctorService.AddDoctorAsync(newDoctor);
            return Ok();
        }

        [HttpDelete("{idDoctor}")]
        public async Task<IActionResult> RemoveDoctorAsync(int idDoctor)
        {
            if (await _doctorService.DeleteDoctorAsync(idDoctor))
            { 
                return Ok();
            }
            else
            {
                return BadRequest("Doctor not found");
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> ModifyDoctorAsync(DoctorDTOREQ newDoctor, int idDoctor)
        {

            if (await _doctorService.ModifyDoctorAsync(newDoctor, idDoctor))
            {
                return Ok();
            }
            else 
            {
                return BadRequest("Doctor not found");
            }
        }

    }
}
