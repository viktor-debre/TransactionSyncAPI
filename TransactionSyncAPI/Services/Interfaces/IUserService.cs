using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> FindUserByName(string? firstName, string? lastName);
    }
}
