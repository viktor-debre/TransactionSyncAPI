using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.DataAccess
{
    public class TransactionContext : DbContext
    {
        public TransactionContext()
        { 
        }

        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
