using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface ITransactionCRUDService
    {
        public Task<IEnumerable<Transaction>> GetAllTransactionFromDb();

        public Task<Transaction?> GetTransactionByIdFromDb(int id);
    }
}
