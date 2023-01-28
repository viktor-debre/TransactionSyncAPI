using Dapper;
using System.Data;
using TransactionSyncAPI.DataAccess.Interfases;

namespace TransactionSyncAPI.DataAccess
{
    public class TransactionWriteDbConnection : ITransactionWriteDbConnection
    {
        private readonly ITransactionDbContext context;
        public TransactionWriteDbConnection(ITransactionDbContext context)
        {
            this.context = context;
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await context.Connection.ExecuteAsync(sql, param, transaction);
        }
    }
}
