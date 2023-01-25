using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class TransactionCRUDService : ITransactionCRUDService
    {
        private readonly ITransactionDbContext _dbContext;
        private readonly ITransactionReadDbConnection _readDbConnection;
        private readonly ITransactionWriteDbConnection _writeDbConnection;

        public TransactionCRUDService(
            ITransactionDbContext dbContext,
            ITransactionReadDbConnection readDbConnection,
            ITransactionWriteDbConnection writeDbConnection)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionFromDb()
        {
            var sqlQuery = "SELECT * FROM transactions";
            var transactions = await _readDbConnection.QueryAsync<Transaction>(sqlQuery);
            return transactions;
        }

        public async Task<Transaction?> GetTransactionByIdFromDb(int id)
        {
            var transaction = await _dbContext.Transactions
                .Include(a => a.CreatedByUser)
                .Where(a => a.TransactionId == id)
                .FirstOrDefaultAsync();

            return transaction;
        }
    }
}
