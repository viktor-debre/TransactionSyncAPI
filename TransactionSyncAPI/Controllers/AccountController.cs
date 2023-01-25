using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Route("api/account")]
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
        public async Task<IActionResult> Login(LoginModel userData)
        {
            var token = await _authService.AuthenticateUser(userData.Email, userData.Password);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
