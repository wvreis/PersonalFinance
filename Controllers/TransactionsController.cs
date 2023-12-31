﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.Helpers.APIs;
using PersonalFinance.Models;
using PersonalFinance.Queries;
using PersonalFinance.RequestModels;
using PersonalFinance.Services;

namespace PersonalFinance.Controllers;

[Route(TransactionsAPI.ApiRoute.URL)]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly AppDb _context;
    private readonly ISpellCheckerService _spellCheckerService;

    public TransactionsController(AppDb context, ISpellCheckerService spellCheckerService)
    {
        _context = context;
        _spellCheckerService = spellCheckerService;
    }

    #region GET
    [Route($"{nameof(GetTransactions)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions([FromQuery] TransactionSearchModel searchModel)
    {
        try {
            if (_context.Transactions == null) {
                return NotFound();
            }

            string searchVectorInfo = _spellCheckerService.GetSpellCheckedSearchVectorString(searchModel.SearchInfo);

            var result = await _context.Transactions
                .WhereDefaultFilters(searchModel, searchVectorInfo)
                .ToListAsync();

            return Ok(result);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetTotalOutgoingAmountForPeriod)}")]
    [HttpGet]
    public async Task<ActionResult<(double pending, double completed, double canceled)>> GetTotalOutgoingAmountForPeriod([FromQuery] TransactionSearchModel searchModel)
    {
        try {
            if (_context.Transactions == null) {
                return NotFound();
            }

            var (pendingAmount, completedAmount, canceledAmount) = await GetTotalAmounts(searchModel, TransactionNature.Outbound);

            return Ok((pendingAmount, completedAmount, canceledAmount));
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetTotalIncomingAmountForPeriod)}")]
    [HttpGet]
    public async Task<ActionResult<(double pending, double completed, double canceled)>> GetTotalIncomingAmountForPeriod([FromQuery] TransactionSearchModel searchModel)
    {
        try {
            if (_context.Transactions == null) {
                return NotFound();
            }

            var (pendingAmount, completedAmount, canceledAmount) = await GetTotalAmounts(searchModel, TransactionNature.Inbound);

            return Ok((pendingAmount, completedAmount, canceledAmount));
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    async Task<List<Tuple<TransactionStatus, double>>> GetAmountByStatus(TransactionSearchModel searchModel, TransactionNature nature)
    {
        string searchVectorInfo = _spellCheckerService.GetSpellCheckedSearchVectorString(searchModel.SearchInfo);

        return await _context.Transactions
            .WhereDefaultFilters(searchModel, searchVectorInfo, nature)
            .GroupBy(t => t.Status)
            .Select(g => new Tuple<TransactionStatus, double>(g.Key, g.Sum(t => t.Amount)))            
            .ToListAsync();
    }

    private async Task<(double pending, double completed, double canceled)> GetTotalAmounts(TransactionSearchModel searchModel, TransactionNature nature)
    {
        var amounts = await GetAmountByStatus(searchModel, nature);

        var pendingAmount = amounts.Where(a => a.Item1 == TransactionStatus.Pending).SingleOrDefault()?.Item2 ?? 0;
        var completedAmount = amounts.Where(a => a.Item1 == TransactionStatus.Completed).SingleOrDefault()?.Item2 ?? 0;
        var canceledAmount = amounts.Where(a => a.Item1 == TransactionStatus.Canceled).SingleOrDefault()?.Item2 ?? 0;

        return (pendingAmount, completedAmount, canceledAmount);
    }

    [Route($"{nameof(GetTransaction)}/{{id}}")]
    [HttpGet]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        try {
            if (_context.Transactions == null) {
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

            var banks = await _context.Banks.ToListAsync();
            return Ok(banks);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetActiveAccounts)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetActiveAccounts()
    {
        try {
            if (_context.Accounts == null) {
                return NotFound();
            }

            var result = await _context.Accounts
                .WhereStatus(true)
                .ToListAsync();

            return Ok(result);
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

            return await _context.Accounts.ToListAsync();
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

            if (transaction.Status == TransactionStatus.Canceled) {
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

            _context.Entry(transaction).Property(x => x.Status).CurrentValue = TransactionStatus.Canceled;

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
