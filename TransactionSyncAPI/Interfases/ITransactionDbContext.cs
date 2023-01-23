using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Interfases
{
    public interface ITransactionDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }

        DbSet<User> Users { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
