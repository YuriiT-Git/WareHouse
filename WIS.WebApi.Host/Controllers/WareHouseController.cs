using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WarehouseInventorySystem.Models;
using WIS.Application.DTO;
using WIS.Application.Features.GetInventoryItemInfo;
using WIS.Application.Features.ReceiveInventoryItem;
using WIS.Application.Features.ShipInventoryItem;

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
    [ProducesResponseType(typeof(List<InventoryItemInfoDto>), 200)]
    public async Task<IActionResult> GetStocksList(CancellationToken cancellationToken)
    {
        var query = new GetAllInventoryItemsRequest();
        var result = await medistR.SendAsync(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("details/{code}")]
    [ProducesResponseType(typeof(InventoryItemInfoDto), 200)]
    public async Task<IActionResult> GetInventoryItemDetails(string code, CancellationToken cancellationToken)
    {
        var query = new GetInventoryDetailsRequest { Code = code };
        var result = await medistR.SendAsync(query, cancellationToken);
        return Ok(result);
    }
}