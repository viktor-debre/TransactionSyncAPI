using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class AuthService : IAuthService
    {
        private readonly TransactionDbContext _context;
        private readonly IGenerationJWTService _jwtService;

        public AuthService(TransactionDbContext context, IGenerationJWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await GetUser(email, password);
            if (user != null)
            {
                var token = _jwtService.GenerateToken(user);
                return token;
            }

            return null;
        }

        private async Task<User> GetUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);

            return user;
        }
    }
}
