using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface ICsvFilesService
    {
        public IEnumerable<Transaction> ImportCsvFile(string path);

        public string ExportCsvFile(IEnumerable<Transaction> transactions);
    }
}
