using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface IAuthenticationJWTService
    {
        public Task<string> GenerateToken(User user);
    }
}
