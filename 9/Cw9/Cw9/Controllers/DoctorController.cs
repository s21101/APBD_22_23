using Cw9.DTO.Request;
using Cw9.DTO.Responce;
using Cw9.Models;
using Cw9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw9.Controllers
{
    [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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
