using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.DTOs;
using PersonalFinance.Helpers.APIs;
using PersonalFinance.Models;
using PersonalFinance.Queries;

namespace PersonalFinance.Controllers;

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

    private List<PanelItem> MapTransactionGroupsToItems(List<IGrouping<int, Transaction>> transactionGroups)
    {
        return transactionGroups.Select(group => new PanelItemDTO {
            Month = group.First().Date.ToString("MMMM"),
            Amount = group.Sum(t => t.Amount)
        }).ToList();
    }
    #endregion
}
