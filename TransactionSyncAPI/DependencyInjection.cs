using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Services.Intarfaces;
using TransactionSyncAPI.Services.Realization;
using TransactionSyncAPI.SQL;

namespace TransactionSyncAPI
{
    public static class DependencyInjection
    {
        public static void AddServises(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationJWTService, AuthenticationJWTService>();
        }

        public static void AddDapperDbConnections(IServiceCollection services)
        {
            services.AddScoped<ITransactionDbContext>(provider => provider.GetService<TransactionDbContext>());
            services.AddScoped<ITransactionWriteDbConnection, TransactionWriteDbConnection>();
            services.AddScoped<ITransactionReadDbConnection, TransactionReadDbConnection>();
        }

        public static void AddSqlQueriesProvider(IServiceCollection services)
        {
            services.AddSingleton<SQLQueriesReader>();
        }
    }
}
