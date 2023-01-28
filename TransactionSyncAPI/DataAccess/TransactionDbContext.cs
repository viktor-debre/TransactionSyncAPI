using Microsoft.EntityFrameworkCore;
using System.Data;
using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.DataAccess
{
    public class TransactionDbContext : DbContext, ITransactionDbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users")
                .HasKey(key => key.Id);

            modelBuilder.Entity<Transaction>().ToTable("transactions")
                .HasKey(key => key.TransactionId);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionId)
                .ValueGeneratedOnAdd();

        }
    }
}
