using Microsoft.AspNetCore.Mvc;
using System.Text;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvSynchronizationController : ControllerBase
    {
        private readonly ICsvFilesService _csvFilesService;
        private readonly ITransactionService _transactionService;

        public CsvSynchronizationController(ICsvFilesService csvFilesService, ITransactionService transactionService)
        {
            _csvFilesService = csvFilesService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsvFile()
        {
            IEnumerable<Transaction> transactions = await _transactionService.GetAllTransactionFromDb();

            string csvData = _csvFilesService.ExportCsvFile(transactions);

            var fileBytes = Encoding.UTF8.GetBytes(csvData);
            return File(fileBytes, "text/csv", "TransactionsData.csv");
        }
    }
}
