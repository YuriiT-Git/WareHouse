using MedistR.Abstractions;

namespace WIS.Application.Features.ShipInventoryItem;

public class RegisterOutgoingStockCommand: IRequest<RegisterOutgoingStockCommand>
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}