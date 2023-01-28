using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> FindUserByName(string? firstName = null, string? lastName = null)
        {
            var user = await _userService.FindUserByName(firstName, lastName);

            return Ok(user);
        }
    }
}
