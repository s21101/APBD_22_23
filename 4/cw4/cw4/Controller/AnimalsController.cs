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
            IEnumerable<Animal> res = null;
            try
            {
                res = await _dbService.GetAnimals(orderBy);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal newAnimal)
        {
            return Ok(await _dbService.AddAnimalAsync(newAnimal));
        }





        [HttpDelete("idAnimal")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            try
            {
                await _dbService.DeleteAnimalAsync(idAnimal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
