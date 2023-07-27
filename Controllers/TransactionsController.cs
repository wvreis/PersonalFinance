using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.Helpers.APIs;
using PersonalFinance.Models;
using Restap.Helpers;
using System.Security.Principal;

namespace PersonalFinance.Controllers;

[Route(TransactionsAPI.ApiRoute.URL)]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly AppDb _context;

    public TransactionsController(AppDb context)
    {
        _context = context;
    }

    #region GET
    [Route($"{nameof(GetTransactions)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(string? searchInfo = null)
    {
        try {
            if (_context.Transactions == null) {
                return NotFound();
            }

            searchInfo = API.UnprocessString(searchInfo);

            var result = _context.Transactions
                .Include(x => x.TransactionType)
                .ToList();

            return Ok(result);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetTransaction)}/{{id}}")]
    [HttpGet]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        try {
            if(_context.Transactions == null) {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null) {
                return NotFound();
            }

            return Ok(transaction);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetBanks)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
    {
        try {
            if (_context.Banks == null) {
                return NotFound();
            }

            return await _context.Banks.ToListAsync();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetAccounts)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        try {
            if (_context.Accounts == null) {
                return NotFound();
            }

            return await _context.Accounts
                .Where(acc =>  acc.Status)
                .ToListAsync();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetTransactionTypes)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionType>>> GetTransactionTypes()
    {
        try {
            if (_context.TransactionTypes == null) {
                return NotFound();
            }

            var result = await _context.TransactionTypes
                .Include(x => x.TransactionTypeGroup)
                .ToListAsync();

            return result;
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
    #endregion

    #region PUT
    [Route($"{nameof(PutTransaction)}/{{id}}")]
    [HttpPut]
    public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
    {
        try {
            if (id != transaction.Id) {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex) {
            if (!TransactionExists(id)) {
                return NotFound();
            }
            else {
                return BadRequest(ex.Message);
            }
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    [Route($"{nameof(PutCancelTransaction)}/{{id}}")]
    [HttpPut]
    public async Task<IActionResult> PutCancelTransaction(int id)
    {
        try {
            var transaction = await _context.Transactions.FindAsync(id);

            transaction.Status = TransactionStatus.Canceled;

            _context.Entry(transaction).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex) {
            if (!TransactionExists(id)) {
                return NotFound();
            }
            else {
                return BadRequest(ex.Message);
            }
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
    #endregion

    #region POST
    [Route(nameof(PostTransaction))]
    [HttpPost]
    public async Task<ActionResult<Account>> PostTransaction(Transaction transaction)
    {
        try {
            if (_context.Transactions == null) {
                return Problem("Entity set 'AppDb.Accounts' is null.");
            }

            if (transaction.Status == TransactionStatus.Canceled) {
                return BadRequest("The provided data is not valid or has a malformed structure.");
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
    #endregion

    #region DELETE
    [Route($"{nameof(DeleteTransaction)}/{{id}}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        if (_context.Transactions == null) {
            return NotFound();
        }
        var account = await _context.Transactions.FindAsync(id);
        if (account == null) {
            return NotFound();
        }

        _context.Transactions.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    #endregion

    private bool TransactionExists(int id)
    {
        return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
