using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Globalization;
using System.Text;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Services.Realization
{
    public class CsvFilesService : ICsvFilesService
    {
        public string ExportCsvFile(IEnumerable<Transaction> transactions)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (var writer = new StringWriter(stringBuilder))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("Id");
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
