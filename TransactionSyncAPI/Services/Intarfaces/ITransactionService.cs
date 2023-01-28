using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface ITransactionService
    {
        public Task<IEnumerable<Transaction>> GetAllTransactionFromDb();

        public Task<Transaction?> GetTransactionByIdFromDb(int id);

        public Task<IEnumerable<Transaction>> GetFilteredTransactions(IEnumerable<string> types = null, string? status = null);

        public Task<Transaction?> SetNewStatusById(int id, string status);

        public Task<IEnumerable<Transaction>> UpdateAndAddNewTransactions(IEnumerable<Transaction> transactions);
    }
}
