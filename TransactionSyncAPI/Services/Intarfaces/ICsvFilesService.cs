using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface ICsvFilesService
    {
        public string ExportCsvFile(IEnumerable<Transaction> transactions);
    }
}
