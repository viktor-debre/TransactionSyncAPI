using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Interfaces
{
    public interface ICsvFilesService
    {
        public IEnumerable<Transaction> ImportCsvFile(string path);

        public string ExportCsvFile(IEnumerable<Transaction> transactions);
    }
}
