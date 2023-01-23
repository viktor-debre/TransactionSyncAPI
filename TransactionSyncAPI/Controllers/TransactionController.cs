using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionSyncAPI.Interfases;
using TransactionSyncAPI.Models;

namespace TransactionSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDbContext _dbContext;
        private readonly ITransactionReadDbConnection _readDbConnection;
        private readonly ITransactionWriteDbConnection _writeDbConnection;

        public TransactionController(
            ITransactionDbContext dbContext,
            ITransactionReadDbConnection readDbConnection,
            ITransactionWriteDbConnection writeDbConnection)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = $"SELECT * FROM transactions";
            var transactions = await _readDbConnection.QueryAsync<Transaction>(query);
            return Ok(transactions);
        }
    }
}
