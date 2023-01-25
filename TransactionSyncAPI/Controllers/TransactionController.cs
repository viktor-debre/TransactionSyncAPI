using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionSyncAPI.Services.Intarfaces;

namespace TransactionSyncAPI.Controllers
{
    [Authorize]
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionCRUDService _transactionService;

        public TransactionController(ITransactionCRUDService transactionService)
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

            return BadRequest("Wrong id");
        }

        [HttpGet]
        [Route("/transactions/filtered")]
        public async Task<IActionResult> Get([FromQuery] IEnumerable<string> types, [FromQuery] string status)
        {
            var transactions = await _transactionService.GetFilteredTransactions(types, status);

            return Ok(transactions);
        }
    }
}
