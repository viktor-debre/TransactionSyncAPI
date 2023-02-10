using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Services.Interfaces;
using TransactionSyncAPI.Services.Realization;
using TransactionSyncAPI.Services.Interfaces.InternalServices;
using TransactionSyncAPI.Services.Realization.InternalServices;

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

            //internal services
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
        }

        public static void AddDapperDbConnections(IServiceCollection services)
        {
            services.AddScoped<ITransactionDbContext>(provider => provider.GetService<TransactionDbContext>());
            services.AddScoped<ITransactionWriteDbConnection, TransactionWriteDbConnection>();
            services.AddScoped<ITransactionReadDbConnection, TransactionReadDbConnection>();
        }
    }
}