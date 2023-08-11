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

            string searchVectorInfo = _spellCheckerService.GetSpellCheckedSearchVectorString(searchModel.SearchInfo);

            var pendingTask = GetAmountByStatus(searchModel, TransactionStatus.Pending, TransactionNature.Outbound);
            var completedTask = GetAmountByStatus(searchModel, TransactionStatus.Completed, TransactionNature.Outbound);
            var canceledTask = GetAmountByStatus(searchModel, TransactionStatus.Canceled, TransactionNature.Outbound);

            await Task.WhenAll(pendingTask, completedTask, canceledTask);

            var pendingAmount = await pendingTask;
            var completedAmount = await completedTask;
            var canceledAmount = await canceledTask;

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

            var pendingTask = GetAmountByStatus(searchModel, TransactionStatus.Pending, TransactionNature.Inbound);
            var completedTask = GetAmountByStatus(searchModel, TransactionStatus.Completed, TransactionNature.Inbound);
            var canceledTask = GetAmountByStatus(searchModel, TransactionStatus.Canceled, TransactionNature.Inbound);

            await Task.WhenAll(pendingTask, completedTask, canceledTask);

            var pendingAmount = await pendingTask;
            var completedAmount = await completedTask;
            var canceledAmount = await canceledTask;

            return Ok((pendingAmount, completedAmount, canceledAmount));
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    async Task<double> GetAmountByStatus(TransactionSearchModel searchModel,  TransactionStatus status, TransactionNature nature)
    {
        string searchVectorInfo = _spellCheckerService.GetSpellCheckedSearchVectorString(searchModel.SearchInfo);
        searchModel.Status = status;

        return await _context.Transactions
            .WhereDefaultFilters(searchModel, searchVectorInfo, nature)
            .SumAsync(t => t.Amount);
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
                .Where(acc => acc.Status)
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
