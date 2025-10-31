using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WarehouseInventorySystem.Models;
using WIS.Application.Features.AuditLog;
using WIS.Application.Features.ReceiveInventoryItem;
using WIS.Application.Features.ShipInventoryItem;
using WIS.Domain.Events;

namespace WarehouseInventorySystem.Controllers;

[Route("api/warehouse")]
public class WareHouseController(IMedistR medistR) : ControllerBase
{
    [HttpPost("register-incoming-stock")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RegisterIncomingStock([FromBody] RegisterIncomingStock registerIncomingStock, CancellationToken cancellationToken)
    {
        var command = new RegisterIncomingStockRequest
        {
            Code = registerIncomingStock.Code,
            Quantity = registerIncomingStock.Quantity
        };
        
        await medistR.SendAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("register-outgoing-stock")]
    public async Task<IActionResult> RegisterOutgoingStock([FromBody] RegisterOutgoingStockModel registerOutgoingStock, CancellationToken cancellationToken)
    {
        
        var command = new RegisterOutgoingStockRequest
        {
            Code = registerOutgoingStock.Code,
            Quantity = registerOutgoingStock.Quantity
        };
        
        await medistR.SendAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<StockUpdatedEvent>), 200)]
    public async Task<IActionResult> GetStocksList(CancellationToken cancellationToken)
    {
        var query = new GetAllInventoryItemsRequest();
        var result = await medistR.SendAsync(query, cancellationToken);
        return Ok(result);
    }
}