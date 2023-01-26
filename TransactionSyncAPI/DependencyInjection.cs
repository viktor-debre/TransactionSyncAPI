using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Services.Intarfaces;
using TransactionSyncAPI.Services.Realization;

namespace TransactionSyncAPI
{
    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IGenerationJWTService, GenerationJWTService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICsvFilesService, CsvFilesService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddDapperDbConnections(IServiceCollection services)
        {
            services.AddScoped<ITransactionDbContext>(provider => provider.GetService<TransactionDbContext>());
            services.AddScoped<ITransactionWriteDbConnection, TransactionWriteDbConnection>();
            services.AddScoped<ITransactionReadDbConnection, TransactionReadDbConnection>();
        }
    }
}
