using WIS.Application.Common.Common.DTO;
using WIS.Domain.Abstractions;
using WIS.Domain.Entities;

namespace WIS.Application.Common.Common.Abstractions;

public interface IEventStorageRepository
{
    Task AddAsync(IDomainEvent @event, CancellationToken cancellation);
    Task<InventoryItemAggregate> GetAsync(string skuNumber, CancellationToken cancellation);
    Task<InventoryItemDto?> GetByDateTimeAsync(string skuNumber, DateTimeOffset dateTime, CancellationToken cancellation);
}