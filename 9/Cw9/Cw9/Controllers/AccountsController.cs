using Cw9.DTO.Request;
using Cw9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountServices)
        {
            _accountsService = accountServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO newUser)
        {
            var res =  await _accountsService.RegisterUser(newUser);
            if (!res)
            {
                return BadRequest("User with that login already exists");
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginRequest)
        {
            var res = await _accountsService.Login(loginRequest);

            if (res == null)
            {
                return NotFound("Invalid username or password");
            }
            return Ok(res);
        }

    }
}
