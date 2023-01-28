using CsvHelper;
using System.Globalization;
using System.Text;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class CsvFilesService : ICsvFilesService
    {
        public IEnumerable<Transaction> ImportCsvFile(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var transactions = new List<Transaction>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var transaction = new Transaction
                    {
                        TransactionId = csv.GetField<int>("TransactionId"),
                        Content = csv.GetField("Content"),
                        Type = csv.GetField("Type"),
                        Status = csv.GetField("Status"),
                        UserId = csv.GetField<int>("UserId")
                    };
                    transactions.Add(transaction);
                }
                return transactions;
            }
        }

        public string ExportCsvFile(IEnumerable<Transaction> transactions)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (var writer = new StringWriter(stringBuilder))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("TransactionId");
                csv.WriteField("Content");
                csv.WriteField("Type");
                csv.WriteField("Status");
                csv.WriteField("UserId");
                csv.NextRecord();

                foreach (var transaction in transactions)
                {
                    csv.WriteField(transaction.TransactionId);
                    csv.WriteField(transaction.Content);
                    csv.WriteField(transaction.Type);
                    csv.WriteField(transaction.Status);
                    csv.WriteField(transaction.UserId);
                    csv.NextRecord();
                }
            }
            string csvString = stringBuilder.ToString();

            return csvString;
        }
    }
}
