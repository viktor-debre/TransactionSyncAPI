using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;
using TransactionSyncAPI.SQL;

namespace TransactionSyncAPI.Services.Realization
{
    public class TransactionCRUDService : ITransactionCRUDService
    {
        private readonly ITransactionDbContext _dbContext;
        private readonly ITransactionReadDbConnection _readDbConnection;
        private readonly ITransactionWriteDbConnection _writeDbConnection;
        private readonly SQLQueriesReader _sqlQueries;

        public TransactionCRUDService(
            ITransactionDbContext dbContext,
            ITransactionReadDbConnection readDbConnection,
            ITransactionWriteDbConnection writeDbConnection,
            SQLQueriesReader sqlQueries)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
            _sqlQueries = sqlQueries;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionFromDb()
        {
            var query = _sqlQueries.Queries["selectAllTransaction"];
            var transactions = await _readDbConnection.QueryAsync<Transaction>(query);
            return transactions;
        }

        public async Task<Transaction> GetTransactionByIdFromDb(int id)
        {
            var transaction = await _dbContext.Transactions
                .Include(a => a.CreatedByUser)
                .Where(a => a.TransactionId == id)
                .FirstOrDefaultAsync();
            return transaction;
        }
    }
}
