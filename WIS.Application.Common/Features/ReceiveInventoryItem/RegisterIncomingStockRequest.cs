using MedistR.Abstractions;

namespace WIS.Application.Common.Features.ReceiveInventoryItem;

public class RegisterIncomingStockRequest: IRequest
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}