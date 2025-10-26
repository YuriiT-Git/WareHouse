using MedistR.Abstractions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockCommand: IRequest<RegisterIncomingStockCommand>
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}