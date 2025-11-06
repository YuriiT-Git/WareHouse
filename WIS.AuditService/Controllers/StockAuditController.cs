using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WIS.Application.Features.AuditLog;
using WIS.Domain.Events;

namespace WIS.AuditService.Controllers;

[Route("api/stockaudit")]
public class StockAuditController(IMedistR medistR) : ControllerBase
{
    [HttpGet("details/{code}")]
    [ProducesResponseType(typeof(StockUpdatedEvent[]), 200)]
    public async Task<IActionResult> GetInventoryItemDetails(string code, CancellationToken cancellationToken)
    {
        var query = new GetAuditForStockRequest { Code = code };
        var result = await medistR.SendAsync(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("get-low-stock-items")]
    [ProducesResponseType(typeof(StockUpdatedEvent[]), 200)]
    public async Task<IActionResult> GetSmallAmountItems(CancellationToken cancellationToken)
    {
        var query = new GetLowStockItemsRequest();
        var result = await medistR.SendAsync(query, cancellationToken);
        return Ok(result);
    }
}