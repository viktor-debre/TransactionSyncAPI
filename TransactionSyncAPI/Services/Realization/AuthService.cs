using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Interfaces;
using TransactionSyncAPI.Services.Interfaces.InternalServices;

namespace TransactionSyncAPI.Services.Realization
{
    public class AuthService : IAuthService
    {
        private readonly TransactionDbContext _context;
        private readonly IGenerationJWTService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            TransactionDbContext context,
            IGenerationJWTService jwtService,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> AuthenticateUser(LoginModel userData)
        {
            var user = await GetUserByEmail(userData.Email);

            if (user == null)
            {
                return null;
            }

            var passwordHash = _passwordHasher.ComputeHash(userData.Password, user.PasswordSalt);
            if( passwordHash != user.PasswordHash)
            {
                return null;
            }

            var token = _jwtService.GenerateToken(user);
            return token;
        }

        public async Task<User?> RegisterUser(RegisterUserModel registerData)
        {
            if (string.IsNullOrEmpty( registerData.Email) || string.IsNullOrEmpty(registerData.Password))
            {
                return null;
            }

            var user = await GetUserByEmail(registerData.Email);

            if (user != null)
            {
                return null;
            }

            var newUser = new User()
            {
                Email = registerData.Email,
                PasswordSalt = _passwordHasher.GenerateSalt(),
                FirstName = registerData.FirstName,
                LastName = registerData.LastName
            };

            newUser.PasswordHash = _passwordHasher.ComputeHash(registerData.Password, newUser.PasswordSalt);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            return user;
        }
    }
}
