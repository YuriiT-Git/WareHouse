using Microsoft.AspNetCore.Mvc;
using Nexus;
using WarehouseInventorySystem.Models;
using WIS.Application.Commands;
using WIS.Application.DTO;
using WIS.Application.Queries;

namespace WarehouseInventorySystem.Controllers;

[Route("api/warehouse")]
public class WareHouseController(INexus nexus) : ControllerBase
{
    [HttpPost("register-incoming-stock")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RegisterIncomingStock([FromBody] RegisterIncomingStock registerIncomingStock, CancellationToken cancellationToken)
    {
        var command = new RegisterIncomingStockCommand
        {
            Code = registerIncomingStock.Code,
            Quantity = registerIncomingStock.Quantity
        };
        
        var result = await nexus.SendAsync(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("register-outgoing-stock")]
    public async Task<IActionResult> RegisterOutgoingStock([FromBody] RegisterOutgoingStockModel registerOutgoingStock, CancellationToken cancellationToken)
    {
        
        var command = new RegisterOutgoingStockCommand
        {
            Code = registerOutgoingStock.Code,
            Quantity = registerOutgoingStock.Quantity
        };
        
        var result = await nexus.SendAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<InventoryItemInfoDto>), 200)]
    public async Task<IActionResult> GetStocksList(CancellationToken cancellationToken)
    {
        var query = new GetAllInventoryItems();
        var result = await nexus.SendAsync(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("details/{code}")]
    [ProducesResponseType(typeof(InventoryItemInfoDto), 200)]
    public async Task<IActionResult> GetInventoryItemDetails(string code, CancellationToken cancellationToken)
    {
        var query = new GetInventoryDetailsQuery { Code = code };
        var result = await nexus.SendAsync(query, cancellationToken);
        return Ok(result);
    }
}