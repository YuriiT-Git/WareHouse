using Microsoft.EntityFrameworkCore;
using WIS.Domain.Abstractions;
using WIS.Domain.Entities;
using WIS.Infrastructure.Persistence.Entities;
using WIS.Infrastructure.Persistence.Extensions;
using WIS.Infrastructure.Repositories;

namespace WIS.Infrastructure.Persistence.Repositories;

public class EventStorageRepository(WareHouseDbContext dbContext) : IEventStorageRepository
{
    public async Task AddAsync(IDomainEvent @event, CancellationToken cancellation)
    {
        var eventModel = @event.ToDbModel();
        await dbContext.Set<EventsStorageModel>().AddAsync(eventModel, cancellation);
        await dbContext.SaveChangesAsync(cancellation);
    }

    public async Task<InventoryItemAggregate> GetAsync(string skuNumber, CancellationToken cancellation)
    {
        var dbEventsPerItem = await dbContext
            .Set<EventsStorageModel>()
            .Where(x => x.Code == skuNumber)
            .Select(x=>x.ToDomain())
            .ToListAsync(cancellation);

        return InventoryItemAggregate.FromHistory(dbEventsPerItem);
    }
}