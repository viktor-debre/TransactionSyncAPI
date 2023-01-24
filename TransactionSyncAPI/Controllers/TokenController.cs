using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransactionSyncAPI.Contants;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly TransactionDbContext _context;
        private readonly IAuthenticationJWTService _jwtService;

        public TokenController(
            IConfiguration config,
            TransactionDbContext context,
            IAuthenticationJWTService jwtService)
        {
            _configuration = config;
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginModel userData)
        {
            if (userData != null && userData.Email != null && userData.Password != null)
            {
                var user = await GetUser(userData.Email, userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", user.Email),
                        new Claim("Password", user.Password)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(AuthenticationInfoContants.JWT_TOKEN_EXPIRATION_TIME_MINUTES),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<LoginModel> GetUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
            if (user != null)
            {
                return new LoginModel() { Email = user.Email, Password = user.Password };
            }

            return null;
        }
    }
}
