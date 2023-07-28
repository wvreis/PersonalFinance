using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.Helpers.APIs;
using PersonalFinance.Models;
using Restap.Helpers;
using System.Data.Common;

namespace PersonalFinance.Controllers;

[Route(AccountsAPI.ApiRoute.URL)]
[ApiController]
public class AccountsController : ControllerBase {
    private readonly AppDb _context;

    public AccountsController(AppDb context)
    {
        _context = context;
    }

    #region GET
    [Route($"{nameof(GetAccounts)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts(string? searchInfo = null)
    {
        try {
            if (_context.Accounts == null) {
                return NotFound();
            }

            searchInfo = API.UnprocessString(searchInfo);

            var result = await _context.Accounts
                .Include(x => x.Bank)
                .ToListAsync();

            return Ok(result);
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);
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

            return Ok(account);
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);
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

            return BadRequest(ex.InnerException.Message ?? ex.Message);
        }
    }

    [Route($"{nameof(GetAccountTypes)}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypes()
    {
        try {
            if (_context.AccountTypes == null) {
                return NotFound();
            }

            return await _context.AccountTypes.ToListAsync();
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);
        }
    }
    #endregion

    #region PUT
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
        catch (DbUpdateConcurrencyException ex) {
            if (!AccountExists(id)) {
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
    [Route(nameof(PostAccount))]
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        try {
            if (_context.Accounts == null) {
                return Problem("Entity set 'AppDb.Accounts'  is null.");
            }

            if (!account.Status)
                return BadRequest("The provided data is not valid or has a malformed structure.");

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);
        }
    }
    #endregion

    #region DELETE
    [Route($"{nameof(DeleteAccount)}/{{id}}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        try {
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
        catch (Exception ex) when (ex.InnerException.Message.Contains("23503:")) {

            return BadRequest("Não podemos excluir esse registro agora, pois outros dependem dele. Você pode inativá-lo");
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);
        }
    }
    #endregion

    private bool AccountExists(int id)
    {
        return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
