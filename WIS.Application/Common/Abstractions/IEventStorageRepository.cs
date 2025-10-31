using WIS.Domain.Abstractions;
using WIS.Domain.Entities;

namespace WIS.Infrastructure.Repositories;

public interface IEventStorageRepository
{
    Task AddAsync(IDomainEvent @event, CancellationToken cancellation);
    Task<InventoryItemAggregate> GetAsync(string skuNumber, CancellationToken cancellation);
}