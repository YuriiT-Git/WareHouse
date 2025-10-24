using MedistR.Abstractions;

namespace WIS.Application.Commands;

public class RegisterIncomingStockCommand: IRequest<bool>
{
    public required string Code { get; init; }
    public required int Quantity { get; init; }
}