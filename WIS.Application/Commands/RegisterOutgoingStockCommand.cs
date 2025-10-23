using Nexus.Abstractions;

namespace WIS.Application.Commands;

public class RegisterOutgoingStockCommand: IRequest<bool>
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}