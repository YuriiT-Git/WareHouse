using MedistR.Abstractions;
using WIS.Application.AuditService.DTO;
using WIS.Application.Common.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetAuditForStockRequestHandler(IInventoryAuditLogRepository repository)
    : IRequestHandler<GetAuditForStockRequest, AuditLogDto>
{
    
    public async Task<AuditLogDto> Handle(GetAuditForStockRequest command, CancellationToken cancellationToken)
    {
        return await repository.GetAsync(command.Code, cancellationToken);
    }
}