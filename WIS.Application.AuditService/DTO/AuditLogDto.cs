using WIS.Domain.ValueObjects.Enums;

namespace WIS.Application.AuditService.DTO;

public class AuditLogDto
{
    public required string Code { get; init; }
    public required ProductType ProductType { get; init; }
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required ItemSize Size { get; init; }
    public required string Color { get; init; }
    public required int Quantity { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}