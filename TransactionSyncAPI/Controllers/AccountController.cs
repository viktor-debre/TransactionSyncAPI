using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Route("api/account/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IActionResult> Login(LoginModel userData)
        {
            var token = await _authService.AuthenticateUser(userData);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        [HttpPost]
        [ActionName("register")]
        public async Task<IActionResult> Register(RegisterUserModel userData)
        {
            var user = await _authService.RegisterUser(userData);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("Invalid credential");
        }
    }
}
