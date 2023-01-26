using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface IGenerationJWTService
    {
        public string? GenerateToken(User user);
    }
}
