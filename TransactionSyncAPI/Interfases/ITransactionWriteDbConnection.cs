using System.Data;

namespace TransactionSyncAPI.Interfases
{
    public interface ITransactionWriteDbConnection
    {
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
