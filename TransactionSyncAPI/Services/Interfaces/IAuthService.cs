using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string?> AuthenticateUser(LoginModel user);

        public Task<User?> RegisterUser(RegisterUserModel registerUser);
    }
}
