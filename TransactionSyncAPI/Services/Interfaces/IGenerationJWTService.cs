using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Interfaces
{
    public interface IGenerationJWTService
    {
        public string? GenerateToken(User user);
    }
}
