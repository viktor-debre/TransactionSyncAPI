using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface ITransactionCRUDService
    {
        public Task<IEnumerable<Transaction>> GetAllTransactionFromDb();

        public Task<Transaction?> GetTransactionByIdFromDb(int id);

        public Task<IEnumerable<Transaction>> GetFilteredTransactions(IEnumerable<string> types = null, string status = null);
    }
}
