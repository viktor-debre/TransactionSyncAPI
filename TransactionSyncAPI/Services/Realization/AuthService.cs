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

        public async Task<string?> AuthenticateUser(LoginModel userData)
        {
            var user = await GetUserWithRightPassword(userData.Email, userData.Password);
            if (user != null)
            {
                var token = _jwtService.GenerateToken(user);
                return token;
            }

            return null;
        }

        public async Task<User?> RegisterUser(RegisterUserModel registerUser)
        {
            if (string.IsNullOrEmpty( registerUser.Email) || string.IsNullOrEmpty(registerUser.Password))
            {
                return null;
            }

            var user = await GetUserByEmail(registerUser.Email);

            if (user == null)
            {
                var newUser = new User()
                {
                    Email = registerUser.Email,
                    Password = registerUser.Password,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }

            return null;
        }

        private async Task<User?> GetUserWithRightPassword(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user != null && user.Password == password)
            {
                return user;
            }

            return null;
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            return user;
        }
    }
}
