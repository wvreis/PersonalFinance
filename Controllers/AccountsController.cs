using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.Helpers.APIs;
using PersonalFinance.Models;
using Restap.Helpers;

namespace PersonalFinance.Controllers;

[Route(AccountsAPI.ApiRoute.URL)]
[ApiController]
public class AccountsController : ControllerBase {
    private readonly AppDb _context;

    public AccountsController(AppDb context)
    {
        _context = context;
    }
    #region Get
    [Route($"{nameof(GetAccounts)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts(string searchInfo = null)
    {
        try {
            if (_context.Accounts == null) {
                return NotFound();
            }

            searchInfo = API.UnprocessString(searchInfo);

            return await _context.Accounts
                .Include(x => x.Bank)
                .ToListAsync();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(GetAccount)}/{{id}}")]
    [HttpGet]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
        try {
            if (_context.Accounts == null) {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null) {
                return NotFound();
            }

            return account;
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
            if (_context.Accounts == null) {
                return NotFound();
            }

            return await _context.Banks.ToListAsync();
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }
    #endregion

    [Route($"{nameof(PutAccount)}/{{id}}")]
    [HttpPut]
    public async Task<IActionResult> PutAccount(int id, Account account)
    {
        try {
            if (id != account.Id) {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!AccountExists(id)) {
                return NotFound();
            }
            else {
                throw;
            }
        }

        return NoContent();
    }

    [Route(nameof(PostAccount))]
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        try {
            if (_context.Accounts == null) {
                return Problem("Entity set 'AppDb.Accounts'  is null.");
            }
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }
        catch (Exception ex) {

            return BadRequest(ex.Message);
        }
    }

    [Route($"{nameof(DeleteAccount)}/{{id}}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        if (_context.Accounts == null) {
            return NotFound();
        }
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) {
            return NotFound();
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AccountExists(int id)
    {
        return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
