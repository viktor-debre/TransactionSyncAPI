using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Authorize]
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transactions = await _transactionService.GetAllTransactionFromDb();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdFromDb(id);
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return BadRequest("Transaction with this id does not exist.");
        }

        [HttpGet]
        [Route("/transactions/filtered")]
        public async Task<IActionResult> Get(
            [FromQuery][DefaultValue(null)] IEnumerable<string> types,
            [FromQuery][DefaultValue(null)] string status)
        {
            var transactions = await _transactionService.GetFilteredTransactions(types, status);

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var transaction = await _transactionService.SetNewStatusById(id, status);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return BadRequest("Wrong id");
        }
    }
}
