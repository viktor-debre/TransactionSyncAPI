using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> FindUserByName(string? firstName, string? lastName);
    }
}
