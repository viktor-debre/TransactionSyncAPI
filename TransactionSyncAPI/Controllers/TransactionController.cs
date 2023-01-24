using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionSyncAPI.DataAccess.Interfases;
using TransactionSyncAPI.Models;
using TransactionSyncAPI.SQL;

namespace TransactionSyncAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDbContext _dbContext;
        private readonly ITransactionReadDbConnection _readDbConnection;
        private readonly ITransactionWriteDbConnection _writeDbConnection;
        private readonly SQLQueriesReader _sqlQueries;

        public TransactionController(
            ITransactionDbContext dbContext,
            ITransactionReadDbConnection readDbConnection,
            ITransactionWriteDbConnection writeDbConnection,
            SQLQueriesReader sqlQueries)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
            _sqlQueries = sqlQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = _sqlQueries.Queries["selectAllTransaction"];
            var transactions = await _readDbConnection.QueryAsync<Transaction>(query);

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var query = _sqlQueries.Queries["selectAllTransaction"];
            //var transaction = await _readDbConnection.QueryFirstOrDefaultAsync<Transaction>(query, id);

            var transaction = await _dbContext.Transactions.Include(a => a.CreatedByUser).Where(a => a.TransactionId == id).FirstOrDefaultAsync();
            return Ok(transaction);
        }
    }
}
