using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Blazor.Data;
using PersonalFinance.Blazor.DTOs;
using PersonalFinance.Blazor.Helpers.APIs;
using PersonalFinance.Blazor.Models;
using PersonalFinance.Blazor.Queries;

namespace PersonalFinance.Blazor.Controllers;

[Route(PanelAPI.ApiRoute.URL)]
[ApiController]
public class PanelController : ControllerBase {
    private readonly AppDb _context;

    public PanelController(AppDb context)
    {
        _context = context;
    }

    #region GET
    [Route($"{nameof(GetPanelItems)}")]
    [HttpGet]
    public async Task<ActionResult<PanelDTO>> GetPanelItems()
    {
        try {
            PanelDTO panelDTO = new();
            var resultIncoming = await GetTransactionGroups(TransactionNature.Inbound);
            var resultOutgoing = await GetTransactionGroups(TransactionNature.Outbound);

            foreach ( var group in resultIncoming ) {
                panelDTO.SetIncomingItemValue(
                    group.First().Date.ToString("MMMM"),
                    group.Sum(t => t.Amount));    
            }

            foreach (var group in resultOutgoing) {
                panelDTO.SetOutgoingItemValue(
                    group.First().Date.ToString("MMMM"),
                    group.Sum(t => t.Amount));
            }            

            return Ok(panelDTO);        
        }
        catch (Exception ex) {

            return BadRequest(ex.InnerException.Message ?? ex.Message);            
        }
    }

    async Task<List<IGrouping<int, Transaction>>> GetTransactionGroups(TransactionNature nature)
    {
        return await _context.Transactions
            .WhereStatus(TransactionStatus.Completed)
            .WhereNature(nature)
            .GroupBy(t => t.Date.Month)
            .ToListAsync();
    }
    #endregion
}
