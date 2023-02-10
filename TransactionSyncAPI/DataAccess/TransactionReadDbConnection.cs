using Dapper;
using Npgsql;
using System.Data;
using TransactionSyncAPI.DataAccess.Interfases;

namespace TransactionSyncAPI.DataAccess
{
    public class TransactionReadDbConnection : ITransactionReadDbConnection, IDisposable
    {
        private readonly IDbConnection connection;
        public TransactionReadDbConnection(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("PostgresSqlConnectionString"));
        }
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.QuerySingleAsync<T>(sql, param, transaction);
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}