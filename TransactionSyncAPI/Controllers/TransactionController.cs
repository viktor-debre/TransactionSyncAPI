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

            //var transactions = await _dbContext.Transactions.Include(t => t.CreatedByUser).ToListAsync();
            return Ok(transactions);
        }
    }
}
