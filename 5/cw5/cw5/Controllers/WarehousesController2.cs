using cw5.Model;
using cw5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController2 : ControllerBase
    {
        private IDbService _dbService;
        public WarehousesController2(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouseProc(Warehouse warehouse)
        {
            try
            {
                return Ok(await _dbService.AddProductToWarehouseProcedure(warehouse));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
