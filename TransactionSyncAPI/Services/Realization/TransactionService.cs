using Dapper;
using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Interfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionDbContext _dbContext;
        private readonly ITransactionReadDbConnection _readDbConnection;
        private readonly ITransactionWriteDbConnection _writeDbConnection;

        public TransactionService(
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

        public async Task<IEnumerable<Transaction>> GetFilteredTransactions(IEnumerable<string> types = null, string? status = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Types", types);
            parameters.Add("@Status", status);


            var sqlQuery = "SELECT * FROM transactions";
            if (types.Count() != 0 && status != null)
            {
                sqlQuery += " WHERE Type IN @Types AND Status = @Status";
            }
            else if (types.Count() != 0)
            {
                sqlQuery += " WHERE Type IN @Types";
            }
            else if (status != null)
            {
                sqlQuery += " WHERE Status = @Status";
            }

            var transactions = await _readDbConnection.QueryAsync<Transaction>(sqlQuery, parameters);

            return transactions;
        }

        public async Task<Transaction?> SetNewStatusById(int id, string status)
        {
            var sqlUpdateQuery = "UPDATE transactions SET Status = @Status WHERE TransactionId = @Id";
            var parameters = new { Status = status, Id = id };
            var rowAffected = await _writeDbConnection.ExecuteAsync(sqlUpdateQuery, parameters);

            if (rowAffected != 0)
            {
                var sqlFindQuery = "SELECT * FROM transactions WHERE TransactionId = @Id";
                var parameterForSearch = new { Id = id };
                var transaction = await _readDbConnection.QueryFirstOrDefaultAsync<Transaction>(sqlFindQuery, parameterForSearch);

                return transaction;
            }

            return null;
        }

        public async Task<IEnumerable<Transaction>> UpdateAndAddNewTransactions(IEnumerable<Transaction> transactions)
        {
            var sqlQuery = "SELECT * FROM transactions";

            string sqlQueryForSync = @"
            MERGE transactions AS T
            USING (VALUES (@TransactionId, @Content, @Status, @Type, @UserId)) as S(TransactionId, Content, Status, Type, UserId)
            ON T.TransactionId = S.TransactionId
            WHEN MATCHED THEN
                UPDATE SET T.Content = S.Content, T.Status = S.Status, T.Type = S.Type, T.UserId = S.UserId
            WHEN NOT MATCHED THEN
                INSERT (TransactionId, Content, Status, Type, UserId)
                Values (S.TransactionId, S.Content, S.Status, S.Type, S.UserId);";

            foreach (var transaction in transactions)
            {
                var parametersForUpdateOrAdding = new
                {
                    TransactionId = transaction.TransactionId,
                    Content = transaction.Content,
                    Status = transaction.Status,
                    Type = transaction.Type,
                    UserId = transaction.UserId
                };
                await _writeDbConnection.ExecuteAsync(sqlQueryForSync, parametersForUpdateOrAdding);
            }

            var resultTransactions = await _readDbConnection.QueryAsync<Transaction>(sqlQuery) ?? new List<Transaction>();

            return resultTransactions;
        }
    }
}
