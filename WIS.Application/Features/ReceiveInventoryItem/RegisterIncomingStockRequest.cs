using MedistR.Abstractions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockRequest: IRequest<RegisterIncomingStockRequest>
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}