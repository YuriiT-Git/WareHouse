using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WarehouseInventorySystem.Models;
using WIS.Application.Common.Features.ReceiveInventoryItem;
using WIS.Application.Common.Features.ShipInventoryItem;
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
    

}