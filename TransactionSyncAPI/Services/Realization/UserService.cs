using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Interfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class UserService : IUserService
    {
        private readonly ITransactionReadDbConnection _readDbConnection;

        public UserService(ITransactionReadDbConnection readDbConnection)
        {
            _readDbConnection = readDbConnection;
        }

        public async Task<IEnumerable<User>> FindUserByName(string? firstName, string? lastName)
        {
            firstName = firstName ?? string.Empty;
            lastName = lastName ?? string.Empty;
            var sqlQuery = "SELECT * FROM users WHERE \"FirstName\" LIKE @FirstName AND \"LastName\" LIKE @LastName;";
            var parameters = new { FirstName = firstName + '%', LastName = lastName + '%' };

            var users = await _readDbConnection.QueryAsync<User>(sqlQuery, parameters);

            return users;
        }
    }
}