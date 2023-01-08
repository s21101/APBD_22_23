using Cw9.DTO.Responce;
using Cw9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw9.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("{idPrescription}")]
        public async Task<IActionResult> GetPrescription(int idPrescription)
        {
            PrescriptionDTO res = await _prescriptionService.GetPrescriptionAsync(idPrescription);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Prescription not found");
            }
        }

    }
}
