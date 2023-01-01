using cw4.Model;
using cw4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw4.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public AnimalsController(IDbService dbService)
        { 
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimalsAsync(string? orderBy)
        {
            try
            {
                return Ok(await _dbService.GetAnimals(orderBy));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal newAnimal)
        {
            try
            {
                await _dbService.AddAnimalAsync(newAnimal);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal(Animal animal, int idAnimal)
        {
            try
            {
                await _dbService.UpdateAnimalAsync(animal, idAnimal);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("idAnimal")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            try
            {
                await _dbService.DeleteAnimalAsync(idAnimal);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
