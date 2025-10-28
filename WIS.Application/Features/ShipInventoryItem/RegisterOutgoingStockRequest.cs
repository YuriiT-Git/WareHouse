using MedistR.Abstractions;

namespace WIS.Application.Features.ShipInventoryItem;

public class RegisterOutgoingStockRequest: IRequest
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}