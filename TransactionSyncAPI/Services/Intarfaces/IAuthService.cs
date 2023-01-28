using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface IAuthService
    {
        public Task<string?> AuthenticateUser(LoginModel user);

        public Task<User?> RegisterUser(RegisterUserModel registerUser);
    }
}
