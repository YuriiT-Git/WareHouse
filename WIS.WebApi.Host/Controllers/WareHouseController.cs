using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WarehouseInventorySystem.Models;
using WIS.Application.Common.Common.DTO;
using WIS.Application.Common.Features.GetStockLevelByDateTime;
using WIS.Application.Common.Features.ReceiveInventoryItem;
using WIS.Application.Common.Features.ShipInventoryItem;
using WIS.Domain.Abstractions;
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
    
    [HttpPost("get-stock-level-at-time")]
    [ProducesResponseType(typeof(InventoryItemDto), 200)]
    public async Task<IActionResult> GetStockStateByDateTime([FromBody] GetStockLevelByDateTimeRequest request, CancellationToken cancellationToken)
    {
        ValidateModelState(ModelState);
        var result = await medistR.SendAsync(request, cancellationToken);
        return Ok(result);
    }

    private void ValidateModelState(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            var message = string.Join("; ", errors);

            throw new ArgumentException(message);
        }
    }

}