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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICsvFilesService _csvFilesService;
        private readonly ITransactionService _transactionService;

        public CsvSynchronizationController(
            IWebHostEnvironment webHostEnvironment,
            ICsvFilesService csvFilesService,
            ITransactionService transactionService)
        {
            _webHostEnvironment = webHostEnvironment;
            _csvFilesService = csvFilesService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsvFile([FromQuery] IEnumerable<string> types, [FromQuery] string? status)
        {
            IEnumerable<Transaction> transactions = await _transactionService.GetFilteredTransactions(types, status);

            string csvData = _csvFilesService.ExportCsvFile(transactions);

            var fileBytes = Encoding.UTF8.GetBytes(csvData);
            return File(fileBytes, "text/csv", "TransactionsData.csv");
        }

        [HttpPost]
        public async Task<IActionResult> ImportCsvFile(IFormFile file)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "SavedFiles");
            string filePath = Path.Combine(path, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var transactions = _csvFilesService.ImportCsvFile(filePath);

            var updatedTransactions = await _transactionService.UpdateAndAddNewTransactions(transactions);

            return Ok(updatedTransactions);
        }
    }
}
