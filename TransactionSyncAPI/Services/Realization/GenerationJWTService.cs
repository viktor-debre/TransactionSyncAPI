using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransactionSyncAPI.Contants;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Interfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class GenerationJWTService : IGenerationJWTService
    {
        private readonly IConfiguration _configuration;

        public GenerationJWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GenerateToken(User user)
        {
            if (user == null)
            {
                return null;
            }

            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", user.Email),
                        new Claim("Password", user.PasswordHash)

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(AuthenticationInfoContants.JWT_TOKEN_EXPIRATION_TIME_MINUTES),
                signingCredentials: signIn);

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
