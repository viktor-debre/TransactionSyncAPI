using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Services.Intarfaces;
using TransactionSyncAPI.Services.Realization;
using TransactionSyncAPI.SQL;

namespace TransactionSyncAPI
{
    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IGenerationJWTService, GenerationJWTService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionCRUDService, TransactionCRUDService>();
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
