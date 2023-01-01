using cw7.DTO.Requests;
using cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private IDbService _dbService;

        public TripsController(IDbService dbService) 
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDescSortedTripsAsync()
        {
            try
            {
                return Ok(await _dbService.GetDescSortedTripsAsync());
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(RequestClientTripDTO requestClientTripDTO)
        {
            try
            {
               await _dbService.AssignClientToTrip(requestClientTripDTO);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}

